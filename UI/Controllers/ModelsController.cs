using AutoMapper;
using EntitiesCL.EFModels;
using EntitiesCL.OtherModels.DTOs;
using EntitiesCL.OtherModels.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServicesCL.Interfaces.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class ModelsController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IRepoWrapper _repo;

        public ModelsController(IRepoWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #region GET
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] ModelParams modelParams)
        {
            try
            {
                var models = await _repo.VehicleModel.GetAllVehicleModelsAsync(modelParams);

                var vehicleModelsResult = _mapper.Map<IEnumerable<VehicleModelDTO>>(models);

                ViewBag.DPSelectListItem = new SelectList(await _repo.VehicleMake.GetAllMakesForDPSelectListItem(), "Value", "Text", modelParams.MakeIdFilterSelected);

                #region Test_1
                //foreach (var item in models)
                //{
                //    foreach (var dto in vehicleModelsResult)
                //    {
                //        dto.VehicleMakeName = item.VehicleMake.Name;
                //    }
                //} 
                #endregion

                return View();

                //return Ok(vehicleModelsResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleModelById(int id) //Use GUID
        {
            try
            {
                var model = await _repo.VehicleModel.GetVehicleModelByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                var vehicleModelResult = _mapper.Map<VehicleModelDTO>(model);

                return Ok(vehicleModelResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        #region PostPutDelete
        [HttpPost]
        public async Task<IActionResult> CreateVehicleModel([FromBody] VehicleModelCreateDTO vehicleModel)
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

                VehicleModel modelEntity = _mapper.Map<VehicleModel>(vehicleModel);

                _repo.VehicleModel.CreateVehicleModel(modelEntity);
                await _repo.SaveAsync();

                VehicleModelDTO createdVehicleModel = _mapper.Map<VehicleModelDTO>(modelEntity);

                return CreatedAtAction(nameof(GetVehicleModelById), new { id = createdVehicleModel.VehicleModelId }, createdVehicleModel);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateVehicleModel(int id, [FromBody] VehicleModelUpdateDTO vehicleModel)
        {
            try
            {
                if (vehicleModel == null)
                {
                    return BadRequest("VehicleMake Object is NULL");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("ModelState is Invalid");
                }

                VehicleModel modelEntity = await _repo.VehicleModel.GetVehicleModelByIdAsync(id);
                if (modelEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(vehicleModel, modelEntity);

                _repo.VehicleModel.UpdateVehicleModel(modelEntity);
                await _repo.SaveAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteVehicleModel(int id)
        {
            try
            {
                VehicleModel vehicleModel = await _repo.VehicleModel.GetVehicleModelByIdAsync(id);
                if (vehicleModel == null)
                {
                    return NotFound();
                }

                _repo.VehicleModel.DeleteVehicleModel(vehicleModel);
                await _repo.SaveAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

    }
}
