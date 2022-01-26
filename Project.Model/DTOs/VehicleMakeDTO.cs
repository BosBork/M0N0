using Project.Model.DTOs.Common;
using System.Collections.Generic;
namespace Project.Model.DTOs
{
    public class VehicleMakeDTO : VehicleMakeUpdateDTO, IVehicleMakeDTO
    {
        public int VehicleMakeId { get; set; }

        public string ModelCount { get; set; }

        public virtual IEnumerable<IVehicleModelDTO> VehicleModels { get; set; } // include
    }

    public class VehicleMakeUpdateDTO : VehicleMakeCreateDTO, IVehicleMakeUpdateDTO
    {
    }

    public class VehicleMakeCreateDTO : IVehicleMakeCreateDTO
    {
        public string Name { get; set; }

        public string Abrv { get; set; }
    }
}
