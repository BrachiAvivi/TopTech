using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;
namespace Bll
{
    class TechnicianStep
    {
        public Warehouse Warehouse { get; set; }
        public List<Destination> Destinations { get; set; }

        //public TechnicianStep(Employee employee,Warehouse warehouse)
        //{
        //    Destinations.Add(new Destination(employee.Location, new TimeSpan(), 0, new TimeSpan()));
        //    this.Warehouse = warehouse;        
        //}
    }
}
