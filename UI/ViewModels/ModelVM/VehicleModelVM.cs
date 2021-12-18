using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EntitiesCL.EFModels;

namespace UI.ViewModels
{
    //public enum test {};
    public class VehicleModelVM
    {
        [BindNever] //test
        public int VehicleModelId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Abrv { get; set; }

        [Display(Name="Make")]
        public int VehicleMakeId { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }

    }
}
