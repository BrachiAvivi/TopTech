using Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bll
{
    public class OpenBusinessDay
    {
        //צריך משהוא מלמעלה שמנהל את הענינים
        //פונקציה זו מופעלת כל יום בשעה מסוימת
        
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

        public OpenBusinessDay()
        {
            db = ClsDB.Instance;
            company = db.GetCompany();
            //opening new day to save in database
            day = new BusinessDay();
            destinations = db.GetDestinations(day.OpeningTime);
            employeesData = db.GetEmployees();
            int index = 0;
            foreach (var item in employeesData)
            {
                employees.Add(new Destination(index++, item.Location, day.OpeningTime,KindOf.home));
            }
            index = 0;
            foreach (var item in db.GetWarehouses())
            {
                warehouses.Add(new Destination(index++,item.Location, day.ClosingTime,KindOf.warehouse));
            }
            GoogleMapsData = CopyDataFromGoogleMaps(destinations,destinations);
            GoogleMapsDataEmplyee = CopyDataFromGoogleMaps(employees, destinations);
            GoogleMapsDataWarehouse = CopyDataFromGoogleMaps(destinations, warehouses);

            random = new Random();

            OpenDay();
        }

        private TimeSpan[][,] CopyDataFromGoogleMaps(List<Destination> from, List<Destination> to)
        {
            TimeSpan[][,] googleMaps = new TimeSpan[4][,];
            googleMaps[0] = CopyDataFromGoogleMaps(new TimeSpan(7, 0, 0),from,to);
            googleMaps[1] = CopyDataFromGoogleMaps(new TimeSpan(9, 0, 0),from,to);
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

        public void OpenDay()
        {
            FindNearest();
            //all the destinations that can by chosen today
            List<Destination>[] destinations = Simulated_Annealing();
            Save(destinations);
        }

        public void Save(List<Destination>[] destinations)
        {
            for (int i = 0; i < destinations.Length; i++)
            {
                foreach (var destination in destinations[i])
                {
                    Visit visit = new Visit
                    {
                        BusinessDayID = day.BusinessDayID,
                        CallId = destination.Call.CallID,
                        EmploeeID = employeesData[i].EmployeeID,
                        EstimatedTime = destination.Timeline
                    };
                    db.AddVisit(visit);
                    db.UpdateCallStatus(destination.Call, Dal.StatusOf.Inlade);
                }
            }
            db.AddBusinessDay(day);
        }

        //------------------FIND NEAREST DESTIANTIONS-------------------

        /// <summary>
        /// init each destination naer destination
        /// לפי הכלל: אם יעד מבודד - יש בסביבתו רק יעד אחד
        /// יש עדיפות לצרף יעדים אלו יחדיו
        /// </summary>
        private void FindNearest()
        {
            TimeSpan[,] times = GoogleMapsData[1];//9-13 pm
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
                if (count == 1)
                {
                    destinations[i].AddNearestDestination(destinations[j]);
                    destinations[j].AddNearestDestination(destinations[i]);
                }
            }
        }


        //---------------Algoritem------------

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Destination>[] Simulated_Annealing()
        {
            int iterations = 5;
            int step_size = 3;

            bool[] isUsed = new bool[destinations.Count];
            const int temp = 100;
            //best
            List<Destination>[] best = InitFirstStep(isUsed);
            int bestMark = Marking(best);
            //current
            List<Destination>[] curr = CopyStep(best);
            int currMark = bestMark;

            for (int i = 0; i < iterations; i++)
            {
                List<Destination>[] next = CopyStep(curr);
                //גיוון
                bool[] localUsed = CopyIsUsed(isUsed);
                next = NextStep(next, step_size, localUsed);
                int nextMark = Marking(next);
                if (nextMark < bestMark)
                {
                    best = CopyStep(next);
                    bestMark = nextMark;
                }
                double diff = nextMark - currMark;
                double t = temp / (i + 1);
                //לא יודעת
                //
                {
                    curr = next;
                    currMark = nextMark;
                    isUsed = localUsed;
                }
            }
            return best;
        }

        public List<Destination>[] InitFirstStep(bool[] isUsed)
        {
            for (int i = 0; i < isUsed.Length; i++)
                isUsed[i] = false;


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
                    list[list.Count - 1] = destination;
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

        private List<Destination>[] NextStep(List<Destination>[] step, int size, bool[] localUsed)
        {
            for (int i = 0; i < step.Length; i++)
            {
                int r = random.Next(0, size);
                if (r <= step[i].Count - 2)
                {
                    for (int j = 0; j < r; j++)
                    {
                        //Selecting a random number
                        int r_r = random.Next(1, step[i].Count - 1);
                        //sign this destination is not in used
                        localUsed[step[i].ElementAt(r_r).Index] = false;
                        //Delete the destination in the selected location
                        step[i].RemoveAt(r_r);
                        //Selecting another destination(s) in its place
                        Destination destination = FindDestination(step[i].ElementAt(r_r - 1), step[i].ElementAt(r_r), localUsed);
                        while (destination != null)
                        {
                            //insert the selected destination
                            step[i].Insert(r_r, destination);
                            //Increase by 1 because it inserted 1 destination
                            r_r++;
                            //More choice, if possible
                            destination = FindDestination(step[i].ElementAt(r_r - 1), step[i].ElementAt(r_r), localUsed);
                        }
                    }
                }
            }
            return step;
        }

        private int Marking(List<Destination>[] step)
        {
            return step.Sum(x => x.Sum(y => y.Priority));
        }


        private TimeSpan GoogleMaps(Destination source, Destination destination, TimeSpan time)
        {
            if (source.Kind == KindOf.customer && destination.Kind == KindOf.customer)
                return GoogleMapsData[GetTime(time)][source.Index, destination.Index];
            if(source.Kind==KindOf.warehouse)
                return GoogleMapsDataWarehouse[GetTime(time)][source.Index, destination.Index];
            return GoogleMapsDataEmplyee[GetTime(time)][source.Index, destination.Index];
        }

        private int GetTime(TimeSpan time)
        {
            if (time.Hours > 9)
                return 0;
            if (time.Hours > 13)
                return 1;
            if (time.Hours > 16)
                return 2;
            return 3;
        }

        private TimeSpan GoogleMaps(Location source, Location destination, TimeSpan time)
        {
            //data from GoogleMaps site
            return new TimeSpan();
        }
    }
}