using Dal;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Destination(int index, Location location, TimeSpan timeline, KindOf kind)//without duration & priority & call => warehouse / customer
            : this(index, location, new TimeSpan(), 0, timeline, null, kind) { }

        //public Destination(int index, Location location, TimeSpan timeline)//without duration & priority & call => employee
        //    : this(index, location, new TimeSpan(), 0, timeline, null, KindOf.customer) { }

        public Destination(int index, Location location, TimeSpan duration, int priority, TimeSpan timeline, Call call)
            : this(index, location, duration, priority, timeline, call, KindOf.customer) { }
        
        public Destination(int index, Location location, TimeSpan duration, int priority, TimeSpan timeline, Call call, KindOf kind)
        {
            Index = index;
            Location = location;
            Duration = duration;
            Priority = priority;
            Timeline = timeline;
            Call = call;
            Kind = kind;
            Nearest = new List<Destination>();
        }

        public void AddNearestDestination(Destination near)
        {
            if(!Nearest.Contains(near))
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
