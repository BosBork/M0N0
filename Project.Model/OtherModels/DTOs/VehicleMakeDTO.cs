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

        public virtual IEnumerable<IVehicleModelDTO> VehicleModels { get; set; }
    }

    public class VehicleMakeUpdateDTO : VehicleMakeDTO, IVehicleMakeUpdateDTO
    {
    }

    public class VehicleMakeCreateDTO : IVehicleMakeCreateDTO
    {
        public int VehicleMakeId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }
    }
}
