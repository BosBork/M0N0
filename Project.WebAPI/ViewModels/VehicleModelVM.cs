using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.DAL;
using Newtonsoft.Json;

namespace Project.WebAPI.ViewModels
{
    public class VehicleModelVM
    {
        [JsonIgnore]
        public int VehicleModelId { get; set; }

        [JsonProperty(PropertyName = "model name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "model abrv")]
        public string Abrv { get; set; }

        [JsonIgnore]
        public int VehicleMakeId { get; set; }

        [JsonProperty(PropertyName = "make name")]
        public string VehicleMakeName => VehicleMake.Name;

        [JsonIgnore]
        public VehicleMakeVM VehicleMake { get; set; }
    }
}
