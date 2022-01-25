using Newtonsoft.Json;

namespace Project.WebAPI.ReadModels
{
    public class VehicleModel_Read
    {
        [JsonIgnore]
        public int VehicleModelId { get; set; }

        [JsonProperty(PropertyName = "model_name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "model_abrv")]
        public string Abrv { get; set; }

        [JsonIgnore]
        public int VehicleMakeId { get; set; }

        [JsonProperty(PropertyName = "make_name")]
        public string VehicleMakeName => VehicleMake.Name;

        [JsonIgnore]
        public VehicleMake_Read VehicleMake { get; set; }
    }
}
