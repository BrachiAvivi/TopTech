using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CompanyEntryDate { get; set; }
        public Nullable<decimal> LocationX { get; set; }
        public Nullable<decimal> LocationY { get; set; }
        public Location Location { get; set; }


        public Employee_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Employee, Employee_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<Employee_tbl>(this);
        }

        public static Employee DalToDto(Employee_tbl employee)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Employee_tbl, Employee>());
            var mapper = new Mapper(config);
            Employee e= mapper.Map<Employee>(employee);
            e.Location = new Location(e.LocationX, e.LocationY);
            return e;
        }
    }
}
