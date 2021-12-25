using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class Visit
    {
        public int VisitingID { get; set; }
        public int BusinessDayID { get; set; }
        public Nullable<System.TimeSpan> EstimatedTime { get; set; }
        public int CallId { get; set; }
        public int EmploeeID { get; set; }
        public int VisitStatusID { get; set; }

        public Visit_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Visit, Visit_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<Visit_tbl>(this);
        }

        public static Visit DalToDto(Visit_tbl visit)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Visit_tbl, Visit>());
            var mapper = new Mapper(config);
            return mapper.Map<Visit>(visit);
        }
    }
}
