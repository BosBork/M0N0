using Project.Model.DTOs.Common;

namespace Project.Model.DTOs
{
    public class VehicleModelDTO : VehicleModelUpdateDTO, IVehicleModelDTO
    {
        public int VehicleModelId { get; set; }

        public virtual IVehicleMakeDTO VehicleMake { get; set; }
    }

    public class VehicleModelUpdateDTO : VehicleModelCreateDTO, IVehicleModelUpdateDTO
    {
    }

    public class VehicleModelCreateDTO : IVehicleModelCreateDTO
    {
        public string Name { get; set; }

        public string Abrv { get; set; }

        public int VehicleMakeId { get; set; }
    }
}
