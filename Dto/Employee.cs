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
        private decimal LocationX;
        private decimal LocationY;
        public Location Location { get; set; }

        public Employee()
        {
            this.Location = new Location(LocationX, LocationY);
        }

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
            return mapper.Map<Employee>(employee);
        }
    }
}
