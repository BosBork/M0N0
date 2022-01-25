using AutoMapper;
using Project.DAL;
using Project.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Project.Repository.Common.Interfaces.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Project.UI.ViewModels;
using Project.UI.ViewModels.Make;
using Project.Service.Common;
using Project.Model.Common;
using Project.Model.Query;

namespace Project.UI.Controllers
{
    public class MakeController : Controller
    {
        readonly string name_sort = nameof(VehicleMakeVM.Name).ToLower();
        readonly string abrv_sort = nameof(VehicleMakeVM.Abrv).ToLower();

        private readonly IMapper _mapper;
        private readonly IServicesWrapper _servicesWrapper;

        public MakeController(IMapper mapper, IServicesWrapper servicesWrapper)
        {
            _mapper = mapper;
            _servicesWrapper = servicesWrapper;
        }

        #region IndexDetails
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] MakeParams makeParams)
        {
            try
            {
                var DomainToDTO = await _servicesWrapper.VehicleMake.GetAllVehicleMakesAsync(makeParams);

                var DTOToVM = _mapper.Map<PagedList<VehicleMakeVM>>(DomainToDTO);

                ViewBag.CurrentSearch = makeParams.MakeFilter?.Name;

                ViewBag.NameSortParam = makeParams.MakeSort?.OrderBy == name_sort ? $"{name_sort} desc" : name_sort;
                ViewBag.AbrvSortParam = makeParams.MakeSort?.OrderBy == abrv_sort ? $"{abrv_sort} desc" : abrv_sort;

                ViewBag.CurrentSort = makeParams.MakeSort?.OrderBy;

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
        public async Task<IActionResult> GetVehicleMakeById(int id) //Use GUID
        {
            try
            {
                var model = await _servicesWrapper.VehicleMake.GetVehicleMakeByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                VehicleMakeVM DomainToVM = _mapper.Map<VehicleMakeVM>(model);

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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVehicleMake(VehicleMakeCreateVM vehicleMakeCreateVM)
        {
            try
            {
                if (vehicleMakeCreateVM == null)
                {
                    return BadRequest("VehicleMakeCreateVM Object is NULL");
                }

                if (await _servicesWrapper.VehicleMake.FindIfExists(x => x.Name.Equals(vehicleMakeCreateVM.Name)))
                {
                    ModelState.AddModelError(nameof(VehicleMakeVM.Name), "This Make Already Exists");
                    return View(vehicleMakeCreateVM);
                }

                if (ModelState.IsValid)
                {
                    var VMtoDTO = _mapper.Map<IVehicleMakeCreateDTO>(vehicleMakeCreateVM);

                    var createdMake = await _servicesWrapper.VehicleMake.CreateVehicleMake(VMtoDTO);

                    //var get_created_by_id = await _servicesWrapper.VehicleMake.GetVehicleMakeByIdAsync(createdMake);

                    //var DTOtoVM = _mapper.Map<VehicleMakeCreateVM>(/*createdMake*/get_created_by_id);

                    Response.StatusCode = (int)HttpStatusCode.Created;

                    //return RedirectToAction("Details", new { id = DTOtoVM.VehicleMakeId });
                    return RedirectToAction("Details", new { id = createdMake });

                }

                return View(vehicleMakeCreateVM);

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
                var model = await _servicesWrapper.VehicleMake.GetVehicleMakeByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                var DomainToVM = _mapper.Map<VehicleMakeUpdateVM>(model);

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
        public async Task<IActionResult> UpdateVehicleMake(int id, VehicleMakeUpdateVM vehicleMakeUpdateVM)
        {
            try
            {
                if (id != vehicleMakeUpdateVM.VehicleMakeId)
                {
                    return NotFound();
                }

                if (vehicleMakeUpdateVM == null)
                {
                    return BadRequest("VehicleMakeEditVM Object is NULL");
                }

                if (await _servicesWrapper.VehicleMake.FindIfExists(x => x.Name.Equals(vehicleMakeUpdateVM.Name) && !x.VehicleMakeId.Equals(id)))
                {
                    ModelState.AddModelError(nameof(VehicleMakeVM.Name), "This Make Already Exists");
                    return View(vehicleMakeUpdateVM);
                }

                if (ModelState.IsValid)
                {
                    var makeEntity = await _servicesWrapper.VehicleMake.GetVehicleMakeByIdAsync(id);

                    if (makeEntity == null)
                    {
                        return NotFound();
                    }

                    var VMtoDTO = _mapper.Map<IVehicleMakeUpdateDTO>(vehicleMakeUpdateVM);

                    await _servicesWrapper.VehicleMake.UpdateVehicleMake(VMtoDTO);

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return RedirectToAction("Details", new { id });
                }

                return View(vehicleMakeUpdateVM);
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
                var model = await _servicesWrapper.VehicleMake.GetVehicleMakeByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                VehicleMakeVM DomainToVM = _mapper.Map<VehicleMakeVM>(model);

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
        public async Task<IActionResult> DeleteVehicleMake(int id)
        {
            try
            {
                var vehicleMake = await _servicesWrapper.VehicleMake.GetVehicleMakeByIdAsync(id);
                if (vehicleMake == null)
                {
                    return NotFound();
                }

                await _servicesWrapper.VehicleMake.DeleteVehicleMake(vehicleMake);

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
