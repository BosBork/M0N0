using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UI.ViewModels
{
    public class VehicleModelCreateVM
    {
        public int VehicleModelId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Abrv { get; set; }

        [DisplayName("Vehicle Make")]
        [Required(ErrorMessage = "You must chose a make")]
        public int? VehicleMakeId { get; set; }
    }
}
