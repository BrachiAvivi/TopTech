using AutoMapper;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    

    public class Status
    {
        public int StatusID { get; set; }
        public string StatusDetail { get; set; }
        public Nullable<int> AssociatedWith { get; set; }

        public Status_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Status, Status_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<Status_tbl>(this);
        }

        public static Status DalToDto(Status_tbl status)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Status_tbl, Status>());
            var mapper = new Mapper(config);
            return mapper.Map<Status>(status);
        }
    }
}