using Project.Common;
using Project.Common.Enums;
using Project.Model.Common;
using Project.Model.Common.Query.Model;
using Project.Model.DTOs.Common;
using Project.Repository.Common.Interfaces;
using Project.Service.Common;
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

        public async Task<int> CreateVehicleModel(IVehicleModelCreateDTO vehicleModel)
        {
            return await _repo.CreateVehicleModel(vehicleModel);
        }

        public async Task UpdateVehicleModel(IVehicleModelUpdateDTO vehicleModelUpdateDTO)
        {
            await _repo.UpdateVehicleModel(vehicleModelUpdateDTO);
        }

        public async Task DeleteVehicleModel(IVehicleModelDTO vehicleModel)
        {
            await _repo.DeleteVehicleModel(vehicleModel);
        }

        public Task<PagedList<IVehicleModelDTO>> GetAllVehicleModelsAsync(
            IModelFilter modelFilter, IModelSort modelSort, IPagingParamsBase paging, Include include)
        {
            return _repo.GetAllVehicleModelsAsync(modelFilter, modelSort, paging, include);
        }

        public async Task<IVehicleModelDTO> GetVehicleModelByIdAsync(int vehicleModelId)
        {
            return await _repo.GetVehicleModelByIdAsync(vehicleModelId);
        }


    }
}
