using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public enum StatusOf
    {
        AwaitingPlacement = 10,
        Inlade = 20,
        AwaitingService = 30,
        Treatment = 40,
        Done = 50,
        CallCanceled = 60,
        VisitCanceled = 70
    };
}
