using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Common;
using Project.Common.Enums;
using Project.Model;
using Project.Model.DTOs;
using Project.Model.Query.Model;
using Project.Service.Common;
using Project.WebAPI.ReadModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly ILogger<ModelController> _logger;
        private readonly IMapper _mapper;
        private readonly IServicesWrapper _servicesWrapper;

        public ModelController(ILogger<ModelController> logger, IMapper mapper, IServicesWrapper servicesWrapper)
        {
            _logger = logger;
            _mapper = mapper;
            _servicesWrapper = servicesWrapper;
        }

        #region GET

        #region All
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetModels(
            [FromQuery] ModelFilter modelFilter,
            [FromQuery] ModelSort modelSort,
            [FromQuery] PagingParamsBase paging)
        {
            var allModels = await _servicesWrapper.VehicleModel.GetAllVehicleModelsAsync(modelFilter, modelSort, paging, Include.Yes);
            if (!allModels.Any())
            {
                return Ok("No Results Found!");
            }
            return Ok(_mapper.Map<PagedList<VehicleModel_Read>>(allModels)); // "ViewModel" test
        }
        #endregion

        #region ById
        [HttpGet("{id:int}", Name = "GetModel")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetModel(int id) //Use GUID
        {
            var model = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(id);

            if (model == null)
            {
                return NotFound("NOT FOUND");
            }

            return Ok(_mapper.Map<VehicleModel_Read>(model));
        }
        #endregion

        #endregion

        #region CreateUpdateDelete

        #region Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateModel([FromBody] VehicleModelCreateDTO vehicleModel)
        {
            if (vehicleModel == null)
            {
                return BadRequest("VehicleModel Object is NULL");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState is Invalid");
            }

            #region Random
            int[] random_vehicle_make_id = await _servicesWrapper.VehicleMake.FindAllMakeIdsForRandom();
            if (!random_vehicle_make_id.Any(x => x == vehicleModel.VehicleMakeId))
            {
                int index = new Random().Next(random_vehicle_make_id.Count());
                vehicleModel.VehicleMakeId = random_vehicle_make_id[index];
            }
            #endregion

            var createdModelID = await _servicesWrapper.VehicleModel.CreateVehicleModel(vehicleModel);

            return CreatedAtAction(nameof(GetModel), new { id = createdModelID }, $"Model with ID {createdModelID} Created!");
        }
        #endregion

        #region Update
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateModel(int id, [FromBody] VehicleModelUpdateDTO vehicleModel)
        {
            if (vehicleModel == null)
            {
                return BadRequest("VehicleModel Object is NULL");
            }

            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest("ModelState is Invalid");
            }

            var modelEntity = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(id);
            if (modelEntity == null)
            {
                return NotFound("Model Id Doesnt Exist");
            }

            bool makeIdExists = await _servicesWrapper.VehicleMake
                .FindIfExists(x => x.VehicleMakeId.Equals(vehicleModel.VehicleMakeId));
            if (makeIdExists == false)
            {
                return NotFound("Make Id Doesnt Exist");
            }

            _mapper.Map(vehicleModel, modelEntity);

            await _servicesWrapper.VehicleModel.UpdateVehicleModel(modelEntity);

            return NoContent();
        }
        #endregion

        #region Delete
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var vehicleModel = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            await _servicesWrapper.VehicleModel.DeleteVehicleModel(vehicleModel);

            return NoContent();
        }
        #endregion

        #endregion

    }
}
