using Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bll
{
    public class OpenBusinessDay
    {
        //todo צריך משהוא מלמעלה שמנהל את הענינים
        //אחרת פונקציה זו מופעלת כל יום בשעה מסוימת

        //רשימה של קריאות שלא טופלו
        ClsDB db;
        Company company;
        Random random;
        BusinessDay day;
        List<Employee> employeesData;
        List<Destination> employees;
        List<Destination> warehouses;
        List<Destination> destinations;
        TimeSpan[][,] GoogleMapsData;
        TimeSpan[][,] GoogleMapsDataWarehouse;
        TimeSpan[][,] GoogleMapsDataEmplyee;

        private int maxMark;
        private int numberOfMustDestinations;

        public OpenBusinessDay()
        {
            db = ClsDB.Instance;
            //opening new day to save in database
            day = new BusinessDay(db.GetLastBusinessDayIndex() + 1);
            Init();
        }

        public OpenBusinessDay(TimeSpan open, TimeSpan close, List<Employee> choosenEmployees)
        {
            db = ClsDB.Instance;
            day = new BusinessDay(db.GetLastBusinessDayIndex() + 1, open, close);
            employeesData = choosenEmployees;
            Init();
        }
        private void Init()
        {
            db.AddBusinessDay(day);
            company = db.GetCompany();
            destinations = db.GetDestinations(day.OpeningTime);
            maxMark = destinations.Sum(y => y.Priority);
            //Create a destination object from the employees' house
            int index = 0;
            employees = new List<Destination>();
            foreach (var item in employeesData)
            {
                employees.Add(new Destination(index, item.Location, day.ClosingTime, KindOf.home));
                index++;
            }
            //Create a destination object from the warehouses
            index = 0;
            warehouses = new List<Destination>();
            foreach (var item in db.GetWarehouses())
            {
                warehouses.Add(new Destination(index, item.Location, day.OpeningTime, KindOf.warehouse));
                index++;
            }
            GoogleMapsData = CopyDataFromGoogleMaps(destinations, destinations);
            GoogleMapsDataEmplyee = CopyDataFromGoogleMaps(destinations, employees);
            GoogleMapsDataWarehouse = CopyDataFromGoogleMaps(warehouses, destinations);

            random = new Random();
            numberOfMustDestinations = destinations.Count(x => x.Priority == db.PriorityForMustDestinations * (int)x.Duration.TotalMinutes);

        }


        /// <summary>
        /// Creates 4 cost matrix for 4 different time periods
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>return TimeSpan[][,] for time of the way between source to destination</returns>
        private TimeSpan[][,] CopyDataFromGoogleMaps(List<Destination> from, List<Destination> to)
        {
            TimeSpan[][,] googleMaps = new TimeSpan[4][,];
            googleMaps[0] = CopyDataFromGoogleMaps(new TimeSpan(7, 0, 0), from, to);
            googleMaps[1] = CopyDataFromGoogleMaps(new TimeSpan(9, 0, 0), from, to);
            googleMaps[2] = CopyDataFromGoogleMaps(new TimeSpan(14, 0, 0), from, to);
            googleMaps[3] = CopyDataFromGoogleMaps(new TimeSpan(16, 0, 0), from, to);
            return googleMaps;
        }

        private TimeSpan[,] CopyDataFromGoogleMaps(TimeSpan time, List<Destination> from, List<Destination> to)
        {
            TimeSpan zero = new TimeSpan();
            //[from,to]
            TimeSpan[,] googleMaps = new TimeSpan[from.Count, to.Count];
            for (int i = 0; i < from.Count; i++)
            {
                for (int j = 0; j < to.Count; j++)
                {
                    if (from[i] == to[j])//the same place
                        googleMaps[i, j] = zero;
                    else
                        //cost[i,j]=google maps data for traveling time + duration time of working 
                        googleMaps[i, j] = GoogleMaps(from[i].Location, to[j].Location, time);
                }
            }
            return googleMaps;
        }

        //private void checking()
        //{
        //    List<double> l = new List<double>();//drop
        //    List<double> l1 = new List<double>();//drop
        //    for (int j = 1; j < 10; j++)
        //    {
        //        temp = j;
        //        for (int i = 0; i < 500; i++)
        //        {
        //            l.Add(Simulated_Annealing());
        //        }
        //        double max = l.Max();
        //        double min = l.Min();
        //        double avg = l.Average();
        //        l1.Add(avg);

        //        l = new List<double>();
        //    }
        //    double max1 = l1.Max();
        //    double min1 = l1.Min();
        //    double avg1 = l1.Average();


        //    l1 = new List<double>();
        //}

        public void OpenDay()
        {
            FindNearest();
            //all the destinations that can by chosen today
            List<Destination>[] destinations = Simulated_Annealing();
            
            int i = 0;
            for (; i < 100; i++)
            {
                if (!CheckIfAllMustDestinationsChoosen(destinations))
                    destinations = Simulated_Annealing();
            }
            if (i == 100)
            {
                System.Windows.Forms.MessageBox.Show("המערכת לא יכולה להגיע לכל הלקוחות ההכרחיים, יש לתגבר בעובד/ים נוסף/ים");
                return;
            }
            Save(destinations);
        }

        public void Save(List<Destination>[] destinations)
        {
            //--------- saveing destination => visints -----------
            //each technition
            for (int i = 0; i < destinations.Length ; i++)
            {
                //ehch destination of the employee without first - warehose and last - his home
                for (int j = 1; j < destinations[i].Count-1; j++)
                {
                    Destination destination = destinations[i].ElementAt(j);
                    Visit visit = new Visit
                    {
                        BusinessDayID = day.BusinessDayID,
                        CallId = destination.Call.CallID,
                        EmployeeID = employeesData[i].EmployeeID,
                        EstimatedTime = destination.Timeline
                    };
                    db.AddVisit(visit);
                    db.UpdateCallStatus(destination.Call, Dal.StatusOf.Inlade);
                }
            }
        }

        //------------------FIND NEAREST DESTIANTIONS-------------------

        /// <summary>
        /// init each destination naer destination
        /// לפי הכלל: אם יעד מבודד - יש בסביבתו רק יעד אחד
        /// יש עדיפות לצרף יעדים אלו יחדיו
        /// </summary>
        private void FindNearest()
        {
            TimeSpan[,] times = GoogleMapsData[1];//9-13 am
            //The distance in this time range
            TimeSpan min20 = new TimeSpan(0, 20, 0);
            for (int i = 0; i < destinations.Count; i++)
            {
                int count = 0, index = -1, j = 0;
                while (count <= 1 && j < destinations.Count)
                {
                    if (i != j && times[i, j] <= min20)
                    {
                        count++;
                        index = j;
                    }
                    j++;
                }
                //אם יש רק יעד נוסף ברדיוס הקרוב
                if (count == 1)
                    destinations[i].AddNearestDestination(destinations[index]);
            }
        }


        //---------------Algoritem------------
        /// <summary>
        /// הרצתי על המספרים בין 1 ל 100, בכל ההרצות 16 הוציא תוצאה הכי טובה
        /// </summary>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Destination>[] Simulated_Annealing()
        {
            const int iterations = 100;
            const int step_size = 2;// it can change

            const int temp = 16;//ok
            //best
            bool[] bestUsed = new bool[destinations.Count];
            List<Destination>[] best = InitFirstStep(bestUsed);
            double bestMark = Marking(best);
            //current
            List<Destination>[] curr = CopyStep(best);
            double currMark = bestMark;
            bool[] isUsed = CopyIsUsed(bestUsed);
            double e = 0;
            //next
            List<Destination>[] next = null;
            double nextMark = 0;


            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i < iterations; i++)
                {
                    next = CopyStep(curr);
                    //גיוון
                    bool[] localUsed = CopyIsUsed(isUsed);
                    next = NextStep(next, step_size, localUsed);
                    nextMark = Marking(next);
                    if (nextMark > bestMark)
                    {
                        best = CopyStep(next);
                        bestMark = nextMark;
                        bestUsed = CopyIsUsed(localUsed);
                    }
                    double diff = nextMark - currMark;
                    double t = temp / Convert.ToDouble(i + 1);
                    double d = random.NextDouble();
                    e = Math.Exp(diff / t);

                    if (diff > 0 || d < e)
                    {
                        curr = next;
                        currMark = nextMark;
                        isUsed = localUsed;
                    }
                }
                curr = CopyStep(best);
                currMark = bestMark;
                isUsed = CopyIsUsed(bestUsed);
            }
            return best;
        }

        public List<Destination>[] InitFirstStep(bool[] isUsed)
        {
            List<Destination>[] arr = new List<Destination>[employees.Count];
            for (int i = 0; i < employees.Count; i++)
            {
                List<Destination> list = new List<Destination>();
                //first - choose the nearest warehouse
                list.Add(ChoseWarehose(employees[i].Location));
                //at last - the employee's house
                list.Add(employees[i]);

                //The algorithm performs a heuristic selection
                Destination destination = FindDestination(list[0], list[1], isUsed);
                while (destination != null)
                {
                    list.Insert(list.Count - 1, destination);
                    var x = list;//drop
                    destination = FindDestination(list[list.Count - 2], list[list.Count - 1], isUsed);
                }
                arr[i] = list;
            }
            return arr;
        }

        public Destination ChoseWarehose(Location employeeLocation)
        {//A heuristic choice for a warehouse
         //data from google for the first warehose

            //time to check - at mid night

            TimeSpan time = new TimeSpan(0, 0, 0);

            TimeSpan min_distance = GoogleMaps(employeeLocation, warehouses[0].Location, time);
            int min_i = 0;
            for (int i = 1; i < warehouses.Count; i++)
            {
                //data from google
                TimeSpan distance = GoogleMaps(employeeLocation, warehouses[i].Location, time);
                if (distance < min_distance)
                {
                    min_distance = distance;
                    min_i = i;
                }
            }
            return warehouses[min_i];
        }

        private bool[] CopyIsUsed(bool[] isUsed)
        {
            bool[] vs = new bool[isUsed.Length];
            for (int i = 0; i < isUsed.Length; i++)
            {
                vs[i] = isUsed[i];
            }
            return vs;
        }

        private List<Destination>[] CopyStep(List<Destination>[] step)
        {
            List<Destination>[] newStep = new List<Destination>[step.Length];
            for (int i = 0; i < step.Length; i++)
            {
                newStep[i] = new List<Destination>(step[i]);
            }
            return newStep;
        }


        public Destination FindDestination(Destination before, Destination after, bool[] used)
        {
            //before
            foreach (Destination item in before.Nearest)
            {
                if (CheckItIfCanBe(before, after, item, used) != null)
                    return item;
            }
            //after
            foreach (Destination item in after.Nearest)
            {
                if (CheckItIfCanBe(before, after, item, used) != null)
                    return item;
            }
            //random destination
            int r = random.Next(0, used.Length);
            for (int index = r; index < used.Length + r; index++)
            {
                if (CheckItIfCanBe(before, after, destinations[index % used.Length], used) != null)
                    return destinations[index % used.Length];
            }
            //אם עברנו על כל היעדים ועדיין לא מצאנו - אין יעד מתאים
            return null;
        }

        private Destination CheckItIfCanBe(Destination before, Destination after, Destination curr, bool[] used)
        {
            //אם יעד זה לא שובץ
            if (!used[curr.Index])
            {
                //time to traver form the sours destination to that checking destination
                TimeSpan span = before.Timeline + before.Duration + GoogleMaps(before, curr, before.Timeline + before.Duration);
                //בדיקה האם כאשר יתוסף יעד זה,
                //עלויות הנסיעות שמשתנות +  משך זמן הביקור (הקודם והנוכחי), עדיין בתווך האפשרי
                if (span + curr.Duration
                    + GoogleMaps(curr, after, span + curr.Duration) < after.Timeline)
                {
                    //Mark that destination being used
                    used[curr.Index] = true;
                    //update this destination's timelnie
                    curr.Timeline = span;
                    return curr;
                }

            }
            return null;
        }

        private List<Destination>[] NextStep(List<Destination>[] step, int stepSize, bool[] localUsed)
        {
            for (int i = 0; i < step.Length; i++)
            {
                int limit = step[i].Count - 1;
                //Randomly selects 2 numbers whose maximum difference is the step size
                int r1 = random.Next(1, limit);
                int r2 = random.Next(Math.Max(1, r1 - stepSize), Math.Min(limit, r1 + stepSize));
                int min = Math.Min(r1, r2);
                int max = Math.Max(r1, r2);
                if (r1 <= step[i].Count - 2)
                {
                    for (int j = min; j <= max; j++)
                    {
                        localUsed[step[i].ElementAt(min).Index] = false;
                        step[i].RemoveAt(min);

                        Destination destination = FindDestination(step[i].ElementAt(min - 1), step[i].ElementAt(min), localUsed);
                        while (destination != null)
                        {
                            //insert the selected destination
                            step[i].Insert(min, destination);
                            //Increase by 1 because it inserted 1 destination
                            min++;
                            //More choice, if possible
                            destination = FindDestination(step[i].ElementAt(min - 1), step[i].ElementAt(min), localUsed);
                        }
                    }
                }
            }
            return step;
        }

        /// <summary>
        /// Calculates the current step mark
        /// </summary>
        /// <param name="step"></param>
        /// <returns>get marking about all the destinations for all the employees</returns>
        private double Marking(List<Destination>[] step)
        {
            int x = step.Sum(employee => employee.Sum(destination => destination.Priority));
            return x * 100 / (double)maxMark;
        }

        private bool CheckIfAllMustDestinationsChoosen(List<Destination>[] step)
        {
            //y-טכנאי x-יעד של כל טכנאי
            int mustInThatStep = step.Sum(y => y.Count(x => x.Priority == db.PriorityForMustDestinations * (int)x.Duration.TotalMinutes));
            return mustInThatStep == numberOfMustDestinations;
        }
        private TimeSpan GoogleMaps(Destination source, Destination destination, TimeSpan time)
        {
            if (source.Kind == KindOf.customer && destination.Kind == KindOf.customer)
                return GoogleMapsData[GetTime(time)][source.Index, destination.Index];
            if (source.Kind == KindOf.warehouse)
                return GoogleMapsDataWarehouse[GetTime(time)][source.Index, destination.Index];
            return GoogleMapsDataEmplyee[GetTime(time)][source.Index, destination.Index];
        }

        private int GetTime(TimeSpan time)
        {
            if (time.Hours < 9)
                return 0;
            if (time.Hours < 13)
                return 1;
            if (time.Hours < 16)
                return 2;
            return 3;
        }

        private TimeSpan GoogleMaps(Location source, Location destination, TimeSpan time)
        {
            //data from GoogleMaps site

            //-----checking------
            double x = Math.Sqrt(Math.Pow(source.X - destination.X, 2) + Math.Pow(source.Y - destination.Y, 2));
            x *= 10;
            return new TimeSpan(Convert.ToInt32(x) / 60, Convert.ToInt32(x) % 60, 0);
        }
    }
}