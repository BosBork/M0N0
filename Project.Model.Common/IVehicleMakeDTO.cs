using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common
{
    public interface IVehicleMakeDTO
    {
        int VehicleMakeId { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }

        IEnumerable<IVehicleModelDTO> VehicleModels { get; set; }
    }

    public interface IVehicleMakeUpdateDTO : IVehicleMakeDTO
    {
    }

    public interface IVehicleMakeCreateDTO
    {
        int VehicleMakeId { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }
    }
}
