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
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class MakesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepoWrapper _repo;

        public MakesController(IRepoWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] MakeParams makesParams)
        {
            try
            {
                var makes = _repo.VehicleMake.GetAllVehicleMakes(makesParams);

                var vehicleMakesResult = _mapper.Map<IEnumerable<VehicleMakeDTO>>(makes);

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


        [HttpGet]
        public IActionResult GetVehicleMakeById(int id)
        {
            try
            {
                var make = _repo.VehicleMake.GetVehicleMakeById(id);

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


        [HttpGet]
        public IActionResult GetVehiclesMakesById(int id)
        {
            try
            {
                var makesModels = _repo.VehicleMake.GetVehicleMakesModelsById(id);

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










































    }
}
