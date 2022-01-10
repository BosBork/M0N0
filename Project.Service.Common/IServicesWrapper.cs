using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IServicesWrapper
    {
        IMakeService VehicleMake { get; }
        IModelService VehicleModel { get; }
        //Task SaveAsync();
    }
}
