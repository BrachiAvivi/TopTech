using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace Bll
{
    public class Destination
    {
        public int Index { get; }
        public Location Location { get; set; }
        public TimeSpan Duration { get; set; }
        public int Priority { get; set; }
        //Changes while running
        public TimeSpan Timeline { get; set; }
        public Call Call { get; set; }
        public KindOf Kind { get; set; }
        public List<Destination> Nearest { get; set; }

        public Destination(Location location, TimeSpan duration, int priority, TimeSpan timeline, KindOf kind)
            : this(location, duration, priority, timeline, kind, null) { }

        public Destination(Location location, TimeSpan duration, int priority, TimeSpan timeline, KindOf kind, Call call)
        {
            Location = location;
            Duration = duration;
            Priority = priority;
            Timeline = timeline;
            Kind = kind;
            Call = call;
            Nearest = new List<Destination>();
        }

        public Destination(int index, Location location, TimeSpan duration, int priority, Call call, KindOf kind)
        {
            Index = index;
            Location = location;
            Duration = duration;
            Priority = priority;
            Call = call;
            Kind = kind;
            Nearest = new List<Destination>();
        }

        public Destination(int index, Location location, int? duration, int priority, Call call, KindOf customer)
        {
            Index = index;
            Location = location;
            Priority = priority;
            Call = call;
        }

        public void AddNearestDestination(Destination near)
        {
            Nearest.Add(near);
        }

        //public Destination(int index, Location location, TimeSpan duration, int priority, KindOf customer)
        //{
        //    Index = index;
        //    Location = location;
        //    Duration = duration;
        //    Priority = priority;
        //}

        //public Destination(int index, Location location, int priority, KindOf customer)
        //{
        //    Index = index;
        //    Location = location;
        //    Priority = priority;
        //}
    }
}
