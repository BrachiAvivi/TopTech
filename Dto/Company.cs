using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public Nullable<int> EmployeesNumber { get; set; }
        public Nullable<int> CommitmentForSeveralBusinessDays { get; set; }
        public string ManagementPermissionCode { get; set; }


        public Company_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Company, Company_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<Company_tbl>(this);
        }

        public static Company DalToDto(Company_tbl company)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Company_tbl, Company>());
            var mapper = new Mapper(config);
            return mapper.Map<Company>(company);
        }
    }
}
