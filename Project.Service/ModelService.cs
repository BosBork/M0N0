using Project.Common;
using Project.Model.Common;
using Project.Model.OtherModels.Query;
using Project.Repository.Common.Interfaces;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class ModelService : IModelService
    {

        private readonly IModelRepo _repo;
        public ModelService(IModelRepo repo)
        {
            _repo = repo;
        }

        public async Task<IVehicleModelCreateDTO> CreateVehicleModel(IVehicleModelCreateDTO vehicleModel)
        {
            return await _repo.CreateVehicleModel(vehicleModel);
        }

        public async Task DeleteVehicleModel(IVehicleModelDTO vehicleModel)
        {
            await _repo.DeleteVehicleModel(vehicleModel);
        }

        public Task<PagedList<IVehicleModelDTO>> GetAllVehicleModelsAsync(ModelParams modelParams)
        {
            return _repo.GetAllVehicleModelsAsync(modelParams);
        }

        public async Task<IVehicleModelDTO> GetVehicleModelByIdAsync(int vehicleModelId)
        {
            return await _repo.GetVehicleModelByIdAsync(vehicleModelId);
        }

        public async Task<IVehicleModelUpdateDTO> UpdateVehicleModel(IVehicleModelUpdateDTO vehicleModel)
        {
            return await _repo.UpdateVehicleModel(vehicleModel);
        }
    }
}
