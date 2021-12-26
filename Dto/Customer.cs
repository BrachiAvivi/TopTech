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
        public Nullable<decimal> LocationX { get; set; }
        public Nullable<decimal> LocationY { get; set; }
        public Location Location { get; set; }

        public Customer()
        {   }


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
            Customer c =  mapper.Map<Customer>(customer);
            c.Location = new Location(c.LocationX, c.LocationY);
            return c;
        }
    }
}
