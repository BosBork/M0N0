using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Project.Model.Common
{
    public interface IVehicleModelDTO
    {
        int VehicleModelId { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }

        int VehicleMakeId { get; set; }

        IVehicleMakeDTO VehicleMake { get; set; }
    }

    public interface IVehicleModelUpdateDTO : IVehicleModelCreateDTO
    {
        int VehicleModelId { get; set; }
    }

    public interface IVehicleModelCreateDTO
    {
        //int VehicleModelId { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }

        int VehicleMakeId { get; set; }
    }
}
