using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class History
    {
        public int HistoryID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int CallID { get; set; }
        public int StatusID { get; set; }

        public History_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<History, History_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<History_tbl>(this);
        }

        public static History DalToDto(History_tbl history)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<History_tbl, History>());
            var mapper = new Mapper(config);
            return mapper.Map<History>(history);
        }
    }
}
