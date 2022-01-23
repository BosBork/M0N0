using Project.Model.Common;

namespace Project.Model.OtherModels.DTOs
{
    public class VehicleModelDTO : IVehicleModelDTO
    {
        public int VehicleModelId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public int VehicleMakeId { get; set; }

        public virtual IVehicleMakeDTO VehicleMake { get; set; }
    }

    public class VehicleModelUpdateDTO : VehicleModelCreateDTO, IVehicleModelUpdateDTO
    {
        public int VehicleModelId { get; set; }
    }

    public class VehicleModelCreateDTO : IVehicleModelCreateDTO
    {
        //public int VehicleModelId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public int VehicleMakeId { get; set; }
    }
}
