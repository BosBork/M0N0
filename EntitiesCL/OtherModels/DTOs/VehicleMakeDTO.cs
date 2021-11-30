using System;
using System.Collections.Generic;
using System.Text;

namespace EntitiesCL.OtherModels.DTOs
{
    public class VehicleMakeDTO
    {
        public int VehicleMakeId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public IEnumerable<VehicleModelDTO> VehicleModels { get; set; }
    }
}
