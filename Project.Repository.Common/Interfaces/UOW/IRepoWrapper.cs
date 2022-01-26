using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common.Interfaces.UOW
{
    public interface IRepoWrapper
    {
        IMakeRepo VehicleMake { get; }
        IModelRepo VehicleModel { get; }
        Task SaveAsync();
    }
}
