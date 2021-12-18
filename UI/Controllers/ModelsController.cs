using AutoMapper;
using EntitiesCL.EFModels;
using EntitiesCL.Helpers;
using EntitiesCL.OtherModels.DTOs;
using EntitiesCL.OtherModels.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ServicesCL.Interfaces.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.ViewModels;
using System.Net;

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

        #region IndexDetails
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] ModelParams modelParams)
        {
            try
            {
                var domain = await _repo.VehicleModel.GetAllVehicleModelsAsync(modelParams);

                #region TestAutomapper
                //var DomainToDTOTestFromController = new PagedList<VehicleModelDTO>(
                //    domain.Select(u => _mapper.Map<VehicleModel, VehicleModelDTO>(u)).ToList(), 
                //    domain.TotalCount, 
                //    domain.CurrentPage, 
                //    domain.PageSize
                //    ); 
                #endregion

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

                Response.StatusCode = (int)HttpStatusCode.OK;
                return View(DTOToVM);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        } 
        #endregion

        #region Details
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

                Response.StatusCode = (int)HttpStatusCode.OK;
                return View(DomainToVM);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        } 
        #endregion
        #endregion

        #region CreateEditDelete
        #region CREATE
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
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
                Response.StatusCode = (int)HttpStatusCode.OK;
                return View(test);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
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

                    Response.StatusCode = (int)HttpStatusCode.Created;
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

        #region EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id) //Use GUID
        {
            try
            {
                VehicleModel model = await _repo.VehicleModel.GetVehicleModelByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                VehicleModelUpdateVM DomainToVM = _mapper.Map<VehicleModelUpdateVM>(model);

                ViewData["DPSelectListItem"] = new SelectList(await _repo.VehicleMake.GetAllMakesForDPSelectListItem(), "Value", "Text");

                Response.StatusCode = (int)HttpStatusCode.OK;
                return View(DomainToVM);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVehicleModel(int id, VehicleModelUpdateVM vehicleModelUpdateVM)
        {
            try
            {
                if (id != vehicleModelUpdateVM.VehicleModelId)
                {
                    return NotFound();
                }

                if (vehicleModelUpdateVM == null)
                {
                    return BadRequest("VehicleModelEditVM Object is NULL");
                }

                if (ModelState.IsValid)
                {
                    VehicleModel modelEntity = await _repo.VehicleModel.GetVehicleModelByIdAsync(id);

                    if (modelEntity == null)
                    {
                        return NotFound();
                    }

                    VehicleModelUpdateDTO VMtoDTO = _mapper.Map<VehicleModelUpdateDTO>(vehicleModelUpdateVM);
                    VehicleModel DTOtoDomain = _mapper.Map<VehicleModel>(VMtoDTO);
                    //VehicleModel VMToDomain = _mapper.Map<VehicleModel>(vehicleModelEditVM);

                    _repo.VehicleModel.UpdateVehicleModel(DTOtoDomain);
                    await _repo.SaveAsync();
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return RedirectToAction("Details", new { id });
                }

                ViewData["DPSelectListItem"] = new SelectList(await _repo.VehicleMake.GetAllMakesForDPSelectListItem(), "Value", "Text");
                return View(vehicleModelUpdateVM);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        #region DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _repo.VehicleModel.GetVehicleModelByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                VehicleModelVM DomainToVM = _mapper.Map<VehicleModelVM>(model);

                Response.StatusCode = (int)HttpStatusCode.OK;
                return View(DomainToVM);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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

                Response.StatusCode = (int)HttpStatusCode.OK;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        } 
        #endregion
        #endregion

    }
}
