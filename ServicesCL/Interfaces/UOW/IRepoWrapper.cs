using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCL.Interfaces.UOW
{
    public interface IRepoWrapper
    {
        IMakeRepo VehicleMake { get; }
        IModelRepo VehicleModel { get; }
        Task SaveAsync();
    }
}
