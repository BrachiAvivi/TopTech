using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal;

namespace Dto
{
    public class Warehouse
    {
        public int WarehouseID { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> LocationX { get; set; }
        public Nullable<decimal> LocationY { get; set; }
        public Location Location { get; set; }

        public Warehouse()
        {
        }

        public Warehouse_tbl DtoToDal()
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Warehouse, Warehouse_tbl>());
            var mapper = new Mapper(config);
            return mapper.Map<Warehouse_tbl>(this);
        }

        public static Warehouse DalToDto(Warehouse_tbl warehouse)
        {
            var config = new MapperConfiguration(
                x => x.CreateMap<Warehouse_tbl, Warehouse>());
            var mapper = new Mapper(config);
            Warehouse w= mapper.Map<Warehouse>(warehouse);
            w.Location = new Location(w.LocationX, w.LocationY);
            return w;
        }
    }
}
