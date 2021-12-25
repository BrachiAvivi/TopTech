using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class WarehouseAssociation
    {
         public int AssociationID { get; set; }
        public int WarehouseID { get; set; }
        public int CompanyID { get; set; }


        public WarehouseAssociation_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<WarehouseAssociation, WarehouseAssociation_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<WarehouseAssociation_tbl>(this);
        }

        public static WarehouseAssociation DalToDto(WarehouseAssociation_tbl warehouseAssociation)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<WarehouseAssociation_tbl, WarehouseAssociation>());
            var mapper = new Mapper(config);
            return mapper.Map<WarehouseAssociation>(warehouseAssociation);
        }

    }
}
