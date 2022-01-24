using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Project.WebAPI.ViewModels
{
    public class VehicleMakeVM
    {
        [JsonIgnore]
        public int VehicleMakeId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        [JsonProperty(PropertyName = "model count")]
        public string ModelCount => VehicleModels.Count.Equals(0) ? "None" : VehicleModels.Count().ToString();

        [JsonIgnore]
        public ICollection<VehicleModelVM> VehicleModels { get; set; }
    }
}
