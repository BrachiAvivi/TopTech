using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;

namespace Bll
{
    class Step
    {
        public TechnicianStep[] TechnicianSteps { get; set; }
        public int Mark { get; set; }


        //public Step(Warehouse[] warehouses)
        //{
        //    Employee[] employees;
        //    TechnicianSteps = new TechnicianStep[employees.Length];
        //    for (int i = 0; i < employees.Length; i++)
        //    {
        //        TechnicianSteps[i] = new TechnicianStep(employees[i], ChoseWarehose(employees[i], warehouses));
        //    }
        //    Mark = 0;
        //}

        public Warehouse ChoseWarehose(Employee employee, Warehouse[] warehouses)
        {
            double min_distence = /*data from google for the first warehose*/5;
            int min_i=0;
            for (int i = 0; i < warehouses.Length; i++)
            {
                double distence =/*data from google*/5;
                if(distence<min_distence)
                {
                    min_distence = distence;
                    min_i = i;
                }
            }
            return warehouses[min_i];
        }

        public int TechCount()
        {
            return TechnicianSteps.Length;
        }
    }
}
