using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class VehicleModelUpdateVM
    {
        public int VehicleModelId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Abrv { get; set; }

        [Display(Name ="Vehicle Make")]
        [Required(ErrorMessage = "You must chose a make")]
        public int VehicleMakeId { get; set; }
    }
}
