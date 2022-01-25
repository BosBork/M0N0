using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Project.WebAPI.ReadModels
{
    public class VehicleMake_Read
    {
        [JsonIgnore]
        public int VehicleMakeId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        [JsonProperty(PropertyName = "model_count")]
        public string ModelCount => VehicleModels.Count.Equals(0) ? "None" : VehicleModels.Count().ToString();

        [JsonIgnore]
        public ICollection<VehicleModel_Read> VehicleModels { get; set; }
    }
}
