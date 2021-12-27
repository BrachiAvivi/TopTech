using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{

    class SimulatedAnnealing
    {
        OpenBusinessDay day;
        bool[] isUsed;
        Destination[] destinations;
        private Warehouse[] warehouses;
        private static readonly int iterations;
        private static readonly int step_size;
        Employee[] employees;
        Random random = new Random();

        public SimulatedAnnealing(TimeSpan openTime, TimeSpan closeTime, Destination[] destinations)
        {

            //this.openTime = openTime;
            //this.closeTime = closeTime;
            this.destinations = destinations;
        }


        public void Simulated_Annealing()
        {
            const int temp = 100;

            List<Destination>[] best = InitFirstStep();
            int bestMark = Marking(best);
            List<Destination>[] curr = CopyStep(best);
            int currMark = bestMark;

            for (int i = 0; i < iterations; i++)
            {
                List<Destination>[] next = CopyStep(curr);
                //גיוון
                bool[] localUsed = CopyIsUsed();
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

        private List<Destination>[] CopyStep(List<Destination>[] step)
        {
            List<Destination>[] newStep = new List<Destination>[step.Length];
            for (int i = 0; i < step.Length; i++)
            {
                newStep[i] = new List<Destination>(step[i]);
            }
            return newStep;
        }

        private bool[] CopyIsUsed()
        {
            bool[] vs = new bool[isUsed.Length];
            for (int i = 0; i < isUsed.Length; i++)
            {
                vs[i] = isUsed[i];
            }
            return vs;
        }
        public List<Destination>[] InitFirstStep()
        {
            List<Destination>[] arr = new List<Destination>[employees.Length];
            for (int i = 0; i < employees.Length; i++)
            {
                List<Destination> list = new List<Destination>();
                //first - chose the nearest warehouse
                list.Add(new Destination(ChoseWarehose(employees[i]).Location, new TimeSpan(), 0, day.BusinessDay.OpeningTime, KindOf.warehouse));
                //at last - the employee's house
                list.Add(new Destination(employees[i].Location, new TimeSpan(), 0, day.BusinessDay.ClosingTime, KindOf.home));

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

        private int Marking(List<Destination>[] step)
        {
            return step.Sum(x => x.Sum(y => y.Priority));
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


        private Destination CheckItIfCanBe(Destination before, Destination after,Destination curr, bool[] used)
        {
            //אם יעד זה לא שובץ
            if (!used[curr.Index])
            {
                TimeSpan span = before.Timeline + GoogleMaps(before.Location, curr.Location);
                //בדיקה האם כאשר יתוסף יעד זה,
                //עלויות הנסיעות שמשתנות +  משך זמן הביקור (הקודם והנוכחי), עדיין בתווך האפשרי
                if (before.Duration + span + curr.Duration
                    + GoogleMaps(curr.Location, after.Location) < after.Timeline)
                {
                    //Mark that destination being used
                    used[curr.Index] = true;
                    //update this destination's timelnie
                    curr.Timeline = before.Duration + span;
                    return curr;
                }

            }
            return null;
        }


        public Warehouse ChoseWarehose(Employee employee)
        {//A heuristic choice for a warehouse
            //data from google for the first warehose

            //time to check - at mid night
            TimeSpan time = new TimeSpan(0, 0, 0);

            TimeSpan min_distance = GoogleMaps(employee.Location, warehouses[0].Location,time);
            int min_i = 0;
            for (int i = 1; i < warehouses.Length; i++)
            {
                //data from google
                TimeSpan distance = GoogleMaps(employee.Location, warehouses[i].Location,time);
                if (distance < min_distance)
                {
                    min_distance = distance;
                    min_i = i;
                }
            }
            return warehouses[min_i];
        }


        private TimeSpan GoogleMaps(Location source, Location destination, TimeSpan time)
        {
            //data from GoogleMaps site
            return new TimeSpan();
        }





        //------------------FIND NEAREST DESTIANTIONS-------------------
        private TimeSpan[,] CreateCostMat(TimeSpan time, Destination[] destinations)
        {
            TimeSpan zero = new TimeSpan();
            //[from,to]
            TimeSpan[,] times = new TimeSpan[destinations.Length, destinations.Length];
            for (int i = 0; i < destinations.Length; i++)
            {
                for (int j = 0; j < destinations.Length; j++)
                {
                    if (i == j)//the same place
                        times[i, j] = zero;
                    else
                        //cost[i,j]=google maps data for traveling time + duration time of working 
                        times[i, j] = GoogleMaps(destinations[i].Location, destinations[j].Location, time) + destinations[j].Duration;
                }
            }
            return times;
        }

        private void FindNearest(TimeSpan[,] times, List<Destination> destinations)
        {
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
    }
}