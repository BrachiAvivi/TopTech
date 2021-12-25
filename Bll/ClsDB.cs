using Dal;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    //TODO ask the theacher where all the mapping are -סירוקה
    public class ClsDB
    {
        TopTechDBEntities db;
        Company_tbl company;
        public static ClsDB Instance { get; } = new ClsDB();
        private ClsDB()
        {
            db = new TopTechDBEntities();
            //there is just one company
            //todo ask teacher why it save just one company - edilshtein
            company = db.Company_tbl.First();
        }

        public RequestResponse GetServicesRequest()
        {
            return new RequestResponse()
            {
                Data = GetServices(),
                Status = "sucsess",
                Massage = "that all services"
            };
        }

        public List<Service> GetServices()
        {
            return db.Service_tbl.Select(s => Service.DalToDto(s)).ToList();
        }

        public List<Warehouse> GetWarehouses()
        {
            return db.Warehouse_tbl.ToList().Select(item => Warehouse.DalToDto(item)).ToList();
            
        }
        public List<Employee> GetEmployees()
        {
            return db.Employee_tbl.ToList().Select(item => Employee.DalToDto(item)).ToList();
        }

        public RequestResponse GetEmployeesResponse()
        {
            return new RequestResponse()
            {
                Data = GetEmployees(),
                Status = "sucsess",
                Massage = "it's all ok"
            };
        }

        public List<Status> GetStatuses()
        {
            return db.Status_tbl.Select(item => Status.DalToDto(item)).ToList();
        }


        public List<Destination> GetDestinations()
        {
            List<Destination> lst = new List<Destination>();
            List<Call_tbl> calls = db.Call_tbl.Where(x => x.Status_tbl.StatusID == 10).ToList();
            int index = 0;
            foreach (Call_tbl item in calls)
            {
                Customer_tbl customer = item.Customer_tbl;
                int priority = CalculatePriority(item);
                Destination d = new Destination(index, new Location(customer.LocationX, customer.LocationY), item.Service_tbl.Duration, priority, Call.DalToDto(item), KindOf.customer);
                index++;
                lst.Add(d);
            }
            return lst;
        }


        private int CalculatePriority(Call_tbl call)
        {
            //How many days have passed since the calling
            int dayOver = db.BusinessDay_tbl.Last().BusinessDayID - call.BusinessDayID;
            if (dayOver == company.CommitmentForSeveralBusinessDays)
                //מספר גבוה מאוד שיחייב את האלגוריתם לבחור יעד זה דווקא
                return 1000;
            return dayOver;
        }

        private List<Call> GetCalls()
        {
            var calls = db.Call_tbl.Where(x => x.Status_tbl.StatusID == (int)StatusOf.AwaitingPlacement).ToList();
            return (from item in calls
                    select Call.DalToDto(item)).ToList();
        }
    }
}
