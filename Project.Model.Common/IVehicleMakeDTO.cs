using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Project.Model.Common
{
    public interface IVehicleMakeDTO
    {
        int VehicleMakeId { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }

        //string ModelCount { get; set; }

        //[JsonIgnore]
        //[IgnoreDataMember]
        IEnumerable<IVehicleModelDTO> VehicleModels { get; set; } // include
    }

    public interface IVehicleMakeUpdateDTO : IVehicleMakeCreateDTO
    {

    }

    public interface IVehicleMakeCreateDTO
    {
        int VehicleMakeId { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }
    }
}
