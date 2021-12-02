using AutoMapper;
using EntitiesCL.EFModels;
using EntitiesCL.OtherModels.DTOs;
using EntitiesCL.OtherModels.Query;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicesCL.Interfaces.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UI.Controllers
{
    //[Route("api/make")]
    //[ApiController]
    public class MakesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepoWrapper _repo;

        public MakesController(IRepoWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #region Get
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] MakeParams makeParams)
        {
            #region test
            //if (string.IsNullOrEmpty(makeParams.First) || !makeParams.First.All(Char.IsLetterOrDigit))
            //{
            //    return BadRequest("Something Went Wrong With Your Request");
            //}

            //if (string.IsNullOrEmpty(makeParams.First) || !Regex.IsMatch(makeParams.First, @"^[a-zA-Z0-9]+$"))
            //{
            //    return BadRequest("Something Went Wrong With Your Request");
            //}
            #endregion

            try
            {
                var makes = await _repo.VehicleMake.GetAllVehicleMakesAsync(makeParams);

                var vehicleMakesResult = _mapper.Map<IEnumerable<VehicleMakeDTO>>(makes);

                //return View();
                return Ok(vehicleMakesResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

            #region Headers
            //var metadata = new
            //{
            //    makes.TotalCount,
            //    makes.PageSize,
            //    makes.CurrentPage,
            //    makes.TotalPages,
            //    makes.HasNext,
            //    makes.HasPrevious
            //};
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata)); 
            #endregion
        }


        [HttpGet/*("{id}", Name = "VehicleMakeById")*/]
        public async Task<IActionResult> GetVehicleMakeById(int id) //Use GUID
        {
            try
            {
                var make = await _repo.VehicleMake.GetVehicleMakeByIdAsync(id);

                if (make == null)
                {
                    return NotFound("NOT FOUND");
                }

                var vehicleMakeResult = _mapper.Map<VehicleMakeDTO>(make);

                return Ok(vehicleMakeResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet/*("id", Name = "ModelsOfVehicleById")*/]
        public IActionResult GetModelsOfVehicleById(int id)
        {
            try
            {
                var makesModels = _repo.VehicleMake.GetModelsOfVehicleByIdAsync(id);

                if (makesModels == null)
                {
                    return NotFound("NOT FOUND");
                }

                var vehicleMakeResult = _mapper.Map<VehicleMakeDTO>(makesModels);

                return Ok(vehicleMakeResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion

        #region PostPutDelete
        [HttpPost]
        public async Task<IActionResult> CreateVehicleMake([FromBody] VehicleMakeCreateDTO vehicleMake)
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

                VehicleMake makeEntity = _mapper.Map<VehicleMake>(vehicleMake);

                #region test
                //var makeEntity = new VehicleMake()
                //{
                //    Name = vehicleMake.Name,
                //    Abrv = vehicleMake.Abrv
                //}; 
                #endregion

                _repo.VehicleMake.CreateVehicleMake(makeEntity);
                await _repo.SaveAsync();

                VehicleMakeDTO createdVehicleMake = _mapper.Map<VehicleMakeDTO>(makeEntity);

                #region test
                //return CreatedAtRoute("VehicleMakeById", new { id = createdVehicleMake.VehicleMakeId }, createdVehicleMake); 
                #endregion
                return CreatedAtAction(nameof(GetVehicleMakeById), new { id = createdVehicleMake.VehicleMakeId }, createdVehicleMake);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateVehicleMake(int id, [FromBody] VehicleMakeUpdateDTO vehicleMake)
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

                VehicleMake makeEntity = await _repo.VehicleMake.GetVehicleMakeByIdAsync(id);
                if (makeEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(vehicleMake, makeEntity);

                _repo.VehicleMake.UpdateVehicleMake(makeEntity);
                await _repo.SaveAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteVehicleMake(int id)
        {
            try
            {
                VehicleMake vehicleMake = await _repo.VehicleMake.GetVehicleMakeByIdAsync(id);
                if (vehicleMake == null)
                {
                    return NotFound();
                }

                _repo.VehicleMake.DeleteVehicleMake(vehicleMake);
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
