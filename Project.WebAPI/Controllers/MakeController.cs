using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.DAL;
using Project.Model.Common;
using Project.Model.OtherModels.DTOs;
using Project.Model.OtherModels.Query;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeController : ControllerBase
    {
        private readonly ILogger<MakeController> _logger;
        private readonly IMapper _mapper;
        private readonly IServicesWrapper _servicesWrapper;

        public MakeController(ILogger<MakeController> logger, IMapper mapper, IServicesWrapper servicesWrapper)
        {
            _logger = logger;
            _mapper = mapper;
            _servicesWrapper = servicesWrapper;
        }

        #region Get

            #region All
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> GetMakes([FromQuery] MakeParams makeParams)
            {
                //throw new Exception("Forced Exception");
                try
                {
                    var allMakes = await _servicesWrapper.VehicleMake.GetAllVehicleMakesAsync(makeParams);

                    //var allMakesResult = _mapper.Map<IEnumerable<VehicleMakeDTO>>(allMakes);

                    return Ok(allMakes);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Something Went Wrong In The {nameof(GetMakes)}");
                    return StatusCode(500, "Internal server error");
                }
            }
            #endregion

            #region ById
            [HttpGet("{id:int}", Name = "GetMake")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> GetMake(int id)
            {
                try
                {
                    var make = await _servicesWrapper.VehicleMake.GetVehicleMakeByIdAsync(id);

                    if (make == null)
                    {
                        return NotFound("NOT FOUND");
                    }

                    //var makeResult = _mapper.Map<VehicleMakeDTO>(make);

                    return Ok(make);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Something Went Wrong In The {nameof(GetMake)}");
                    return StatusCode(500, "Internal server error");
                }
            }
        #endregion

        #endregion

        #region CreateUpdateDelete

            #region Create
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> CreateMake([FromBody] VehicleMakeCreateDTO vehicleMake)
            {
                try
                {
                    if (vehicleMake == null)
                    {
                        return BadRequest("VehicleMake Object is NULL");
                    }

                    if (!ModelState.IsValid)
                    {
                        return BadRequest("ModelState is Invalid");
                    }

                    var createdMakeID = await _servicesWrapper.VehicleMake.CreateVehicleMake(vehicleMake);

                    return CreatedAtAction(nameof(GetMake), new { id = createdMakeID }, $"Make with ID {createdMakeID} Created!");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Something Went Wrong In The {nameof(CreateMake)}");
                    return StatusCode(500, "Internal server error");
                }
            }
        #endregion

            #region Update
            [HttpPut("{id:int}")]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> UpdateMake(int id, [FromBody] VehicleMakeUpdateDTO vehicleMake)
            {
                try
                {
                    if (vehicleMake == null)
                    {
                        return BadRequest("VehicleMake Object is NULL");
                    }

                    if (!ModelState.IsValid)
                    {
                        return BadRequest("ModelState is Invalid");
                    }

                    var makeEntity = await _servicesWrapper.VehicleMake.GetVehicleMakeByIdAsync(id);
                    if (makeEntity == null)
                    {
                        return NotFound();
                    }

                    vehicleMake.VehicleMakeId = makeEntity.VehicleMakeId;
                    await _servicesWrapper.VehicleMake.UpdateVehicleMake(vehicleMake);

                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Something Went Wrong In The {nameof(UpdateMake)}");
                    return StatusCode(500, "Internal server error");
                }
            }
        #endregion

            #region Delete
            [HttpDelete("{id:int}")]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> DeleteMake(int id)
            {
                try
                {
                    var vehicleMake = await _servicesWrapper.VehicleMake.GetVehicleMakeByIdAsync(id);
                    if (vehicleMake == null)
                    {
                        return NotFound();
                    }

                    await _servicesWrapper.VehicleMake.DeleteVehicleMake(vehicleMake);

                    return NoContent();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Something Went Wrong In The {nameof(UpdateMake)}");
                    return StatusCode(500, "Internal server error");
                }
            } 
            #endregion

        #endregion

    }
}
