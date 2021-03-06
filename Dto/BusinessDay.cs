using AutoMapper;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dto
{
    public class BusinessDay
    {
        public int BusinessDayID { get; set; }
        public int BusinessDayIndex { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public System.TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }


        public BusinessDay(int index)
        {
            this.BusinessDayIndex = index;
            this.Date = DateTime.Today;
            this.OpeningTime = new TimeSpan(10, 0, 0);
            this.ClosingTime = new TimeSpan(16, 0, 0);
        }

        public BusinessDay(int index, TimeSpan openingTime, TimeSpan closingTime)
        {
            this.BusinessDayIndex = index;
            this.Date = DateTime.Today;
            OpeningTime = openingTime;
            ClosingTime = closingTime;
        }


        public TimeSpan TotalTime ()
        {
            return (TimeSpan)(OpeningTime - ClosingTime);
        }

        public BusinessDay_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<BusinessDay, BusinessDay_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<BusinessDay_tbl>(this);
        }

        public static BusinessDay DalToDto(BusinessDay_tbl businessDay)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<BusinessDay_tbl, BusinessDay>());
            var mapper = new Mapper(config);
            return mapper.Map<BusinessDay>(businessDay);
        }
    }
}
