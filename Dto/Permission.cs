using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public int CompanyID { get; set; }
        public string Description { get; set; }

        public Permission_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Permission, Permission_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<Permission_tbl>(this);
        }

        public static Permission DalToDto(Permission_tbl permission)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Permission_tbl, Permission>());
            var mapper = new Mapper(config);
            return mapper.Map<Permission>(permission);
        }
    }
}
