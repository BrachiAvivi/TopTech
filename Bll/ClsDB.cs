using AutoMapper;
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
            //company = db.Company_tbl.First();
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

        internal Company GetCompany()
        {
            return Company.DalToDto(db.Company_tbl.First());
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

        internal void AddVisit(Visit visit)
        {
            db.Visit_tbl.Add(visit.DtoToDal());
        }

        /// <summary>
        /// create destination object from all the calls that awaiting to place
        /// </summary>
        /// <returns>list of destinations</returns>
        public List<Destination> GetDestinations(TimeSpan openTime)
        {
            List<Destination> lst = new List<Destination>();
            List<Call_tbl> calls = db.Call_tbl
                .Where(x => x.Status_tbl.StatusID == Convert.ToInt32(StatusOf.AwaitingPlacement)).ToList();
            int index = 0;
            foreach (Call_tbl item in calls)
            {
                Customer_tbl customer = item.Customer_tbl;
                int priority = CalculatePriority(item);
                Destination d = new Destination(
                    index,
                    new Location(customer.LocationX, customer.LocationY),
                    (TimeSpan)item.Service_tbl.Duration,
                    priority, 
                    openTime,
                    Call.DalToDto(item)
                    );
                index++;
                lst.Add(d);
            }
            return lst;
        }

        /// <summary>
        /// calculate the priority
        /// </summary>
        /// <param name="call"></param>
        /// <returns>The calculation return the number of days that have passed. If this is the last day - a high number will be returned</returns>
        private int CalculatePriority(Call_tbl call)
        {
            //How many days have passed since the calling
            int dayOver = db.BusinessDay_tbl.Last().BusinessDayID - call.BusinessDayID;
            if (dayOver >= company.CommitmentForSeveralBusinessDays)
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

        internal void AddBusinessDay(BusinessDay day)
        {
            db.BusinessDay_tbl.Add(day.DtoToDal());
            db.SaveChanges();
        }

        /// <summary>
        /// update status, save history status and when needed, moved to arcive
        /// </summary>
        /// <param name="call"></param>
        /// <param name="status"></param>
        public void UpdateCallStatus(Call call, StatusOf status)
        {
            //todo איך משנים במסד נתונים?
            //todo שומרים רק את המפתח והוא משלים לבד את העצם?

            if (status == StatusOf.Done || status == StatusOf.CallCanceled)
            {
                var config = new MapperConfiguration
                    (x => x.CreateMap<Call_tbl, Archive_tbl>());
                var mapper = new Mapper(config);
                Archive_tbl archive = mapper.Map<Archive_tbl>(call);
                //insert new archive
                db.Archive_tbl.Add(archive);
                //delete fom calls list
                db.Call_tbl.Remove(call.DtoToDal());
            }
            else
                //change status
                db.Call_tbl.Find(call.CallID).CallStatusID = (int)status;

            History_tbl history = new History_tbl
            {
                CallID = call.CallID,
                Date = DateTime.Now,
                StatusID = (int)status
            };
            //adding to history
            db.History_tbl.Add(history);
            db.SaveChanges();
        }
    }
}
