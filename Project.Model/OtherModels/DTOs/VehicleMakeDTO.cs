using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
namespace Project.Model.OtherModels.DTOs
{
    public class VehicleMakeDTO : IVehicleMakeDTO
    {
        public int VehicleMakeId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public string ModelCount { get; set; }

        public virtual IEnumerable<IVehicleModelDTO> VehicleModels { get; set; } // include
    }

    public class VehicleMakeUpdateDTO : VehicleMakeCreateDTO, IVehicleMakeUpdateDTO
    {
        public int VehicleMakeId { get; set; }
    }

    public class VehicleMakeCreateDTO : IVehicleMakeCreateDTO
    {
        public string Name { get; set; }

        public string Abrv { get; set; }
    }
}
