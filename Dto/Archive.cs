using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    class Archive
    {
        public int CallID { get; set; }
        public int CustomerID { get; set; }
        public int BusinessDayID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Note { get; set; }
        public int CallStatusID { get; set; }
        public Nullable<int> ServiceID { get; set; }

        public Archive(int callID, int customerID, int businessDayID, DateTime? date, string note, int callStatusID, int? serviceID)
        {
            CallID = callID;
            CustomerID = customerID;
            BusinessDayID = businessDayID;
            Date = date;
            Note = note;
            CallStatusID = callStatusID;
            ServiceID = serviceID;
        }

        public Archive_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Archive, Archive_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<Archive_tbl>(this);
        }

        public static Archive DalToDto(Archive_tbl archive)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Archive_tbl, Archive>());
            var mapper = new Mapper(config);
            return mapper.Map<Archive>(archive);
        }
    }
}
