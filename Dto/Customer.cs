using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Phone { get; set; }
        private double LocationX;
        private double LocationY;
        public Location Location { get; set; }

        public Customer()
        {
            this.Location = new Location(LocationX, LocationY);
        }


        public Customer_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Customer, Customer_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<Customer_tbl>(this);
        }

        public static Customer DalToDto(Customer_tbl customer)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Customer_tbl, Customer>());
            var mapper = new Mapper(config);
            return mapper.Map<Customer>(customer);
        }
    }
}
