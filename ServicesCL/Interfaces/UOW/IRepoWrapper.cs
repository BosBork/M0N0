using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCL.Interfaces.UOW
{
    public interface IRepoWrapper
    {
        IMakeRepo VehicleMake { get; }
        IModelRepo VehicleModel { get; }
        void Save();
    }
}
