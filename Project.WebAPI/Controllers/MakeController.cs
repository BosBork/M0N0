using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Common;
using Project.Common.Enums;
using Project.Model.DTOs;
using Project.Model.Query;
using Project.Service.Common;
using Project.WebAPI.ReadModels;
using System;
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
            var allMakes = await _servicesWrapper.VehicleMake.GetAllVehicleMakesAsync(makeParams, Include.Yes);
            return Ok(_mapper.Map<PagedList<VehicleMake_Read>>(allMakes)); // "ViewModel" test
        }
        #endregion

        #region ById
        [HttpGet("{id:int}", Name = "GetMake")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMake(int id)
        {
            var make = await _servicesWrapper.VehicleMake.GetVehicleMakeByIdWithModelsAsync(id);

            if (make == null)
            {
                return NotFound("NOT FOUND");
            }

            return Ok(_mapper.Map<VehicleMake_Read>(make));
        }
        #endregion

        #endregion

        #region CreateUpdateDelete

        #region Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMake([FromBody] VehicleMakeCreateDTO vehicleMake)
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
        #endregion

        #region Update
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMake(int id, [FromBody] VehicleMakeUpdateDTO vehicleMake)
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
        #endregion

        #region Delete
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMake(int id)
        {
            var vehicleMake = await _servicesWrapper.VehicleMake.GetVehicleMakeByIdAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            await _servicesWrapper.VehicleMake.DeleteVehicleMake(vehicleMake);

            return NoContent();
        }
        #endregion

        #endregion

    }
}
