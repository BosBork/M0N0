using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Common.Enums;
using Project.DAL;
using Project.Model.Common;
using Project.Model.OtherModels.DTOs;
using Project.Model.OtherModels.Query;
using Project.Service.Common;
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
            public async Task<IActionResult> GetModels([FromQuery] ModelParams modelParams)
            {
                try
                {
                    var allModels = await _servicesWrapper.VehicleModel.GetAllVehicleModelsAsync(modelParams);

                    //var allModelsResult = _mapper.Map<IEnumerable<VehicleModelDTO>>(allModels);

                    return Ok(allModels);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Something Went Wrong In The {nameof(GetModels)}");
                    return StatusCode(500, "Internal server error");
                }
            }
        #endregion

            #region ById
            [HttpGet("{id:int}", Name = "GetModel")]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> GetModel(int id) //Use GUID
            {
                try
                {
                    var model = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(id);

                    if (model == null)
                    {
                        return NotFound("NOT FOUND");
                    }

                    //var modelResult = _mapper.Map<VehicleModelDTO>(model);

                    return Ok(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Something Went Wrong In The {nameof(GetModel)}");
                    return StatusCode(500, "Internal server error");
                }
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
                try
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
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Something Went Wrong In The {nameof(CreateModel)}");
                    return StatusCode(500, "Internal server error");
                }
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
                try
                {
                    if (vehicleModel == null)
                    {
                        return BadRequest("VehicleModel Object is NULL");
                    }

                    if (!ModelState.IsValid || id < 1)
                    {
                        return BadRequest("ModelState is Invalid");
                    }

                    var makeIdExists = await _servicesWrapper.VehicleMake
                        .FindIfExists(x => x.VehicleMakeId == vehicleModel.VehicleMakeId);
                    if (makeIdExists == false)
                    {
                        return NotFound("Make Id Doesnt Exist");
                    }

                    var modelEntity = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(id);
                    if (modelEntity == null)
                    {
                        return NotFound("Model Id Doesnt Exist");
                    }

                    vehicleModel.VehicleModelId = modelEntity.VehicleModelId;
                    await _servicesWrapper.VehicleModel.UpdateVehicleModel(vehicleModel);

                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Something Went Wrong In The {nameof(UpdateModel)}");
                    return StatusCode(500, "Internal server error");
                }
            }
            #endregion

            #region Delete
            [HttpDelete("{id:int}")]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> DeleteModel(int id)
            {
                try
                {
                    var vehicleModel = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(id);
                    if (vehicleModel == null)
                    {
                        return NotFound();
                    }

                    await _servicesWrapper.VehicleModel.DeleteVehicleModel(vehicleModel);

                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Something Went Wrong In The {nameof(DeleteModel)}");
                    return StatusCode(500, "Internal server error");
                }
            }
            #endregion

        #endregion

    }
}
