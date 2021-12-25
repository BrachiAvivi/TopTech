using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class Call
    {
        public int CallID { get; set; }
        public int CustomerID { get; set; }
        public int BusinessDayID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Note { get; set; }
        public Nullable<int> Priority { get; set; }
        public int CallStatusID { get; set; }
        public Nullable<int> ServiceID { get; set; }


        public Call_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Call, Call_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<Call_tbl>(this);
        }

        public static Call DalToDto(Call_tbl call)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Call_tbl, Call>());
            var mapper = new Mapper(config);
            return mapper.Map<Call>(call);
        }
    }
}
