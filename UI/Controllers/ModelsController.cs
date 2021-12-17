using AutoMapper;
using EntitiesCL.EFModels;
using EntitiesCL.Helpers;
using EntitiesCL.OtherModels.DTOs;
using EntitiesCL.OtherModels.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServicesCL.Interfaces.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI.Controllers
{
    public class ModelsController : Controller
    {
        readonly string name_sort = nameof(VehicleModelVM.Name).ToLower();
        readonly string abrv_sort = nameof(VehicleModelVM.Abrv).ToLower();
        readonly string makeId_sort = nameof(VehicleModelVM.VehicleMakeId);

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
                var domain = await _repo.VehicleModel.GetAllVehicleModelsAsync(modelParams);

                var DomainToDTO = _mapper.Map<PagedList<VehicleModelDTO>>(domain);

                var DTOToVM = _mapper.Map<PagedList<VehicleModelVM>>(DomainToDTO);

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

                ViewBag.CurrentSearch = modelParams.Name;
                ViewBag.CurrentFilter = modelParams.MakeIdFilterSelected;

                ViewBag.NameSortParam = modelParams.OrderBy == name_sort ? $"{name_sort} desc" : name_sort;
                ViewBag.AbrvSortParam = modelParams.OrderBy == abrv_sort ? $"{abrv_sort} desc" : abrv_sort;
                ViewBag.IdSortParam = modelParams.OrderBy == makeId_sort ? $"{makeId_sort} desc" : makeId_sort;

                ViewBag.CurrentSort = modelParams.OrderBy;

                return View(DTOToVM);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet, ActionName("Details")]
        public async Task<IActionResult> GetVehicleModelById(int id) //Use GUID
        {
            try
            {
                VehicleModel model = await _repo.VehicleModel.GetVehicleModelByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                VehicleModelVM DomainToVM = _mapper.Map<VehicleModelVM>(model);

                return View(DomainToVM);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        #region PostPutDelete
        #region CREATE
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            #region RandomFill
            string guid = Guid.NewGuid().ToString();
            string rand = guid.Split('-')[1];

            int[] random_vehicle_make_id = _repo.VehicleMake.FindAll().Select(x => x.VehicleMakeId).ToArray();
            int index = new Random().Next(random_vehicle_make_id.Count());

            var test = new VehicleModelCreateVM()
            {
                Name = $"Test Model {rand}",
                Abrv = $"{rand}",
                VehicleMakeId = random_vehicle_make_id[index]
            };
            #endregion

            ViewBag.DPSelectListItem = new SelectList(await _repo.VehicleMake.GetAllMakesForDPSelectListItem(), "Value", "Text");
            return View(test);
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateVehicleModel(VehicleModelCreateVM vehicleModelCreateVM)
        {
            try
            {
                if (vehicleModelCreateVM == null)
                {
                    return BadRequest("VehicleModelCreateVM Object is NULL");
                }

                if (ModelState.IsValid)
                {
                    VehicleModelCreateDTO VMtoDTO = _mapper.Map<VehicleModelCreateDTO>(vehicleModelCreateVM);
                    VehicleModel DTOtoDomain = _mapper.Map<VehicleModel>(VMtoDTO);

                    _repo.VehicleModel.CreateVehicleModel(DTOtoDomain);
                    await _repo.SaveAsync();

                    VehicleModelCreateVM DomainToVM = _mapper.Map<VehicleModelCreateVM>(DTOtoDomain);
                    return RedirectToAction("Details", new { id = DomainToVM.VehicleModelId });
                }

                ViewBag.DPSelectListItem = new SelectList(await _repo.VehicleMake.GetAllMakesForDPSelectListItem(), "Value", "Text");
                return View(vehicleModelCreateVM);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        } 
        #endregion

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

        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var model = await _repo.VehicleModel.GetVehicleModelByIdAsync(id);

        //        if (model == null)
        //        {
        //            return NotFound("NOT FOUND");
        //        }

        //        var vehicleModelResult = _mapper.Map<VehicleModelDTO>(model);

        //        return Ok(vehicleModelResult);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpDelete, ActionName("Delete")]
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
