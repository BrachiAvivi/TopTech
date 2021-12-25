using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string Detail { get; set; }
        public Nullable<int> Duration { get; set; }

        public Service_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Service, Service_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<Service_tbl>(this);
        }

        public static Service DalToDto(Service_tbl service)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Service_tbl, Service>());
            var mapper = new Mapper(config);
            return mapper.Map<Service>(service);
        }
    }
}
