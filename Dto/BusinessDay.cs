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
        public int CompanyID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }


        public BusinessDay()
        {
            //todo מה זה אומר רק חברה אחת?
            this.Date = DateTime.Today;
            this.OpeningTime = new TimeSpan(10, 0, 0);
            this.ClosingTime = new TimeSpan(16, 0, 0);
        }

        public BusinessDay(TimeSpan openingTime, TimeSpan closingTime)
        {
            this.Date = DateTime.Today;
            OpeningTime = openingTime;
            ClosingTime = closingTime;
        }


        public TimeSpan TotalTime ()
        {
            return OpeningTime - ClosingTime;
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
