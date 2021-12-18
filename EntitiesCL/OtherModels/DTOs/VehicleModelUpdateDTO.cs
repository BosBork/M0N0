using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntitiesCL.OtherModels.DTOs
{
    public class VehicleModelUpdateDTO
    {
        public int VehicleModelId { get; set; }

        //[Required(ErrorMessage = "Name is required")]
        //[MaxLength(100, ErrorMessage = "MaxLength for name is 100")]
        public string Name { get; set; }

        //[MaxLength(100, ErrorMessage = "MaxLength for Abrv is 100")]
        public string Abrv { get; set; }

        public int VehicleMakeId { get; set; }
    }
}
