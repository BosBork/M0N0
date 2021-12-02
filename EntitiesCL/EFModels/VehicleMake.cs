using EntitiesCL.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntitiesCL.EFModels
{
    public class VehicleMake : IOther
    {
        public int VehicleMakeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Abrv { get; set; }

        public ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
