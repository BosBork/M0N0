using AutoMapper;
using Project.DAL;
using Project.Common;
using Project.Model.OtherModels.DTOs;
using Project.Model.OtherModels.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Project.Repository.Common.Interfaces.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.UI.ViewModels;
using System.Net;
using Project.Service.Common;
using Project.Model.Common;

namespace Project.UI.Controllers
{
    public class ModelsController : Controller
    {
        readonly string name_sort = nameof(VehicleModelVM.Name).ToLower();
        readonly string abrv_sort = nameof(VehicleModelVM.Abrv).ToLower();
        readonly string makeId_sort = nameof(VehicleModelVM.VehicleMakeId);

        private readonly IMapper _mapper;
        private readonly IServicesWrapper _servicesWrapper;

        public ModelsController(IMapper mapper, IServicesWrapper servicesWrapper)
        {
            _mapper = mapper;
            _servicesWrapper = servicesWrapper;
        }

        #region IndexDetails
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] ModelParams modelParams)
        {
            try
            {
                var DomainToDTO = await _servicesWrapper.VehicleModel.GetAllVehicleModelsAsync(modelParams);

                #region TestAutomapper
                //var DomainToDTOTestFromController = new PagedList<VehicleModelDTO>(
                //    domain.Select(u => _mapper.Map<VehicleModel, VehicleModelDTO>(u)).ToList(), 
                //    domain.TotalCount, 
                //    domain.CurrentPage, 
                //    domain.PageSize
                //    ); 
                #endregion

                var DTOToVM = _mapper.Map<PagedList<VehicleModelVM>>(DomainToDTO);

                ViewBag.DPSelectListItem = new SelectList(await _servicesWrapper.VehicleMake.GetAllMakesForDPSelectListItem(), "Value", "Text", modelParams.MakeIdFilterSelected);

                ViewBag.CurrentSearch = modelParams.Name;
                ViewBag.CurrentFilter = modelParams.MakeIdFilterSelected;

                ViewBag.NameSortParam = modelParams.OrderBy == name_sort ? $"{name_sort} desc" : name_sort;
                ViewBag.AbrvSortParam = modelParams.OrderBy == abrv_sort ? $"{abrv_sort} desc" : abrv_sort;
                ViewBag.IdSortParam = modelParams.OrderBy == makeId_sort ? $"{makeId_sort} desc" : makeId_sort;

                ViewBag.CurrentSort = modelParams.OrderBy;

                Response.StatusCode = (int)HttpStatusCode.OK;
                return View(DTOToVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: \n {ex.Message}");
            }
        } 
        #endregion

        #region Details
        [HttpGet, ActionName("Details")]
        public async Task<IActionResult> GetVehicleModelById(int id) //Use GUID
        {
            try
            {
                var model = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                VehicleModelVM DomainToVM = _mapper.Map<VehicleModelVM>(model);

                Response.StatusCode = (int)HttpStatusCode.OK;
                return View(DomainToVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: \n {ex.Message}");
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

                int[] random_vehicle_make_id = await _servicesWrapper.VehicleMake.FindAllMakeIdsForRandom();
                int index = new Random().Next(random_vehicle_make_id.Count());

                var test = new VehicleModelCreateVM()
                {
                    Name = $"Test Model {rand}",
                    Abrv = $"{rand}",
                    VehicleMakeId = random_vehicle_make_id[index]
                };
                #endregion
                ViewBag.DPSelectListItem = new SelectList(
                    await _servicesWrapper.VehicleMake.GetAllMakesForDPSelectListItem(), 
                    "Value", "Text");

                Response.StatusCode = (int)HttpStatusCode.OK;
                return View(test);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: \n {ex.Message}");
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
                    var VMtoDTO = _mapper.Map<IVehicleModelCreateDTO>(vehicleModelCreateVM);

                    var createdModel = await _servicesWrapper.VehicleModel.CreateVehicleModel(VMtoDTO);

                    //var DTOtoVM = _mapper.Map<VehicleModelCreateVM>(createdModel);

                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return RedirectToAction("Details", new { id = createdModel });
                }

                ViewBag.DPSelectListItem = new SelectList(
                    await _servicesWrapper.VehicleMake.GetAllMakesForDPSelectListItem(), 
                    "Value", "Text");

                return View(vehicleModelCreateVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: \n {ex.Message}");
            }
        }
        #endregion

        #region EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id) //Use GUID
        {
            try
            {
                var model = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                VehicleModelUpdateVM DomainToVM = _mapper.Map<VehicleModelUpdateVM>(model);

                ViewData["DPSelectListItem"] = new SelectList(
                    await _servicesWrapper.VehicleMake.GetAllMakesForDPSelectListItem(),
                    "Value", "Text");

                Response.StatusCode = (int)HttpStatusCode.OK;
                return View(DomainToVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: \n {ex.Message}");
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
                    var model = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(id);

                    if (model == null)
                    {
                        return NotFound();
                    }

                    var VMtoDTO = _mapper.Map<IVehicleModelUpdateDTO>(vehicleModelUpdateVM);

                    await _servicesWrapper.VehicleModel.UpdateVehicleModel(VMtoDTO);

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return RedirectToAction("Details", new { id });
                }

                ViewData["DPSelectListItem"] = new SelectList(
                    await _servicesWrapper.VehicleMake.GetAllMakesForDPSelectListItem(), 
                    "Value", "Text");

                return View(vehicleModelUpdateVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: \n {ex.Message}");
            }
        }
        #endregion

        #region DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                VehicleModelVM DomainToVM = _mapper.Map<VehicleModelVM>(model);

                Response.StatusCode = (int)HttpStatusCode.OK;
                return View(DomainToVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: \n {ex.Message}");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVehicleModel(int id)
        {
            try
            {
                var vehicleModel = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(id);
                if (vehicleModel == null)
                {
                    return NotFound();
                }

                await _servicesWrapper.VehicleModel.DeleteVehicleModel(vehicleModel);

                Response.StatusCode = (int)HttpStatusCode.OK;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: \n {ex.Message}");
            }
        } 
        #endregion
        #endregion

    }
}
