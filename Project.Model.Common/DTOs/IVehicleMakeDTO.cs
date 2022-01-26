using System.Collections.Generic;

namespace Project.Model.DTOs.Common
{
    public interface IVehicleMakeDTO : IVehicleMakeUpdateDTO
    {
        int VehicleMakeId { get; set; }

        string ModelCount { get; set; }

        //[IgnoreDataMember]
        IEnumerable<IVehicleModelDTO> VehicleModels { get; set; } // include
    }

    public interface IVehicleMakeUpdateDTO : IVehicleMakeCreateDTO
    {
    }

    public interface IVehicleMakeCreateDTO
    {
        string Name { get; set; }

        string Abrv { get; set; }
    }
}
