using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class Location
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Location(decimal x, decimal y)
        {
            X = (double)x;
            Y = (double)y;
        }
        public Location(decimal? x, decimal? y)
        {
            X = (double)x;
            Y = (double)y;
        }
    }
}
