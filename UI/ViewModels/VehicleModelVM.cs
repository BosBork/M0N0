using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace UI.ViewModels
{
    //public enum test {};
    public class VehicleModelVM
    {
        public int VehicleModelId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Abrv { get; set; }

        [DisplayName("Vehicle Make ID")]
        public int VehicleMakeId { get; set; }

    }
}
