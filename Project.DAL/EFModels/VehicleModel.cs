using Project.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.DAL
{
    public class VehicleModel : IQueryHelper
    {
        public int VehicleModelId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Abrv { get; set; }

        public int VehicleMakeId { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }
    }
}
