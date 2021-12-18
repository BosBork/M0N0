﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels.Make
{
    public class VehicleMakeCreateVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "MaxLength for name is 100")]
        public string Name { get; set; }

        public string Abrv { get; set; }
    }
}
