namespace Project.Model.DTOs.Common
{
    public interface IVehicleModelDTO : IVehicleModelUpdateDTO
    {
        int VehicleModelId { get; set; }

        IVehicleMakeDTO VehicleMake { get; set; }
    }

    public interface IVehicleModelUpdateDTO : IVehicleModelCreateDTO
    {
    }

    public interface IVehicleModelCreateDTO
    {
        string Name { get; set; }

        string Abrv { get; set; }

        int VehicleMakeId { get; set; }
    }
}
