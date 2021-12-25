using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    class Removed
    {
        private TimeSpan Round(TimeSpan time)
        {
            if (time.Minutes <= 15)
                return time.Add(new TimeSpan(0, time.Minutes * -1, 0));
            if (time.Minutes <= 30)
                return time.Add(new TimeSpan(0, time.Minutes * -1 + 15, 0));
            if (time.Minutes <= 45)
                return time.Add(new TimeSpan(0, time.Minutes * -1 + 30, 0));
            return time.Add(new TimeSpan(0, time.Minutes * -1 + 45, 0));
        }

        private void Init()
        {
         
            ////array for used?
            //isUsed = new bool[destinations.Length];
            //for (int i = 0; i < destinations.Length; i++)
            //{
            //    isUsed[i] = false;
            //}

            ////array for warehouse
            //warehouseCost = new TimeSpan[warehouses.Length, destinations.Length];
            //for (int i = 0; i < warehouses.Length; i++)
            //{
            //    for (int j = 0; j < destinations.Length; j++)
            //    {
            //        warehouseCost[i, j] =/*google maps data + */ destinations[j].Duration;
            //    }
            //}
        }
        //create mat for costs
        //Cost = travel time + service time
        private void CreateCostMats(TimeSpan openTime, TimeSpan closeTime, Destination[] destinations)
        {
            //TimeSpan open = Round(openTime);
            //TimeSpan close = Round(closeTime);
            //costs = new TimeSpan[Convert.ToInt32((close - open).TotalMinutes) / 15][,];

            ////Google Maps gives data on every quarter of an hour
            //TimeSpan quarter = new TimeSpan(0, 15, 0);
            //int index = 0;
            //while (open < close)
            //{
            //    costs[index++] = CreateCostMat(open, destinations);
            //    //addition quarter of an hour
            //    open += quarter;
            //}
        }

        private TimeSpan[,] CreateCostMat(TimeSpan time, Destination[] destinations)
        {
            TimeSpan zero = new TimeSpan();
            //[from,to]
            TimeSpan[,] cost = new TimeSpan[destinations.Length, destinations.Length];
            for (int i = 0; i < destinations.Length; i++)
            {
                for (int j = 0; j < destinations.Length; j++)
                {
                    if (i == j)//the same place
                        cost[i, j] = zero;
                    else
                        //cost[i,j]=google maps data for traveling time + duration time of working 
                        cost[i, j] =/*google maps data + */ destinations[j].Duration;
                }
            }
            return cost;
        }

    }
}
