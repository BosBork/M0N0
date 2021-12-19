using AutoMapper;
using EntitiesCL.EFModels;
using EntitiesCL.Helpers;
using EntitiesCL.OtherModels.DTOs;
using EntitiesCL.OtherModels.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ServicesCL.Interfaces.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UI.ViewModels;
using UI.ViewModels.Make;

namespace UI.Controllers
{
    public class MakesController : Controller
    {
        readonly string name_sort = nameof(VehicleMakeVM.Name).ToLower();
        readonly string abrv_sort = nameof(VehicleMakeVM.Abrv).ToLower();

        private readonly IMapper _mapper;
        private readonly IRepoWrapper _repo;

        public MakesController(IRepoWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #region IndexDetails
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] MakeParams makeParams)
        {
            try
            {
                var domain = await _repo.VehicleMake.GetAllVehicleMakesAsync(makeParams);

                var DomainToDTO = _mapper.Map<PagedList<VehicleMakeDTO>>(domain);

                var DTOToVM = _mapper.Map<PagedList<VehicleMakeVM>>(DomainToDTO);

                ViewBag.CurrentSearch = makeParams.Name;

                ViewBag.NameSortParam = makeParams.OrderBy == name_sort ? $"{name_sort} desc" : name_sort;
                ViewBag.AbrvSortParam = makeParams.OrderBy == abrv_sort ? $"{abrv_sort} desc" : abrv_sort;

                ViewBag.CurrentSort = makeParams.OrderBy;

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
        public async Task<IActionResult> GetVehicleMakeById(int id) //Use GUID
        {
            try
            {
                VehicleMake model = await _repo.VehicleMake.GetVehicleMakeByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                VehicleMakeVM DomainToVM = _mapper.Map<VehicleMakeVM>(model);

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

                if (_repo.VehicleMake.FindIfExists(x => x.Name.Equals(vehicleMakeCreateVM.Name)))
                {
                    ModelState.AddModelError(nameof(VehicleMakeVM.Name), "This Make Already Exists");
                    return View(vehicleMakeCreateVM);
                }

                if (ModelState.IsValid)
                {
                    VehicleMakeCreateDTO VMtoDTO = _mapper.Map<VehicleMakeCreateDTO>(vehicleMakeCreateVM);
                    VehicleMake DTOtoDomain = _mapper.Map<VehicleMake>(VMtoDTO);

                    _repo.VehicleMake.CreateVehicleMake(DTOtoDomain);
                    await _repo.SaveAsync();

                    VehicleMakeCreateVM DomainToVM = _mapper.Map<VehicleMakeCreateVM>(DTOtoDomain);

                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return RedirectToAction("Details", new { id = DomainToVM.VehicleMakeId });
                }

                return View(vehicleMakeCreateVM);

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
                VehicleMake model = await _repo.VehicleMake.GetVehicleMakeByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                VehicleMakeUpdateVM DomainToVM = _mapper.Map<VehicleMakeUpdateVM>(model);

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

                if (_repo.VehicleMake.FindIfExists(x => x.Name.Equals(vehicleMakeUpdateVM.Name) && !x.VehicleMakeId.Equals(id)))
                {
                    ModelState.AddModelError(nameof(VehicleMakeVM.Name), "This Make Already Exists");
                    return View(vehicleMakeUpdateVM);
                }

                if (ModelState.IsValid)
                {
                    VehicleMake modelEntity = await _repo.VehicleMake.GetVehicleMakeByIdAsync(id);

                    if (modelEntity == null)
                    {
                        return NotFound();
                    }

                    VehicleMakeUpdateDTO VMtoDTO = _mapper.Map<VehicleMakeUpdateDTO>(vehicleMakeUpdateVM);
                    VehicleMake DTOtoDomain = _mapper.Map<VehicleMake>(VMtoDTO);

                    _repo.VehicleMake.UpdateVehicleMake(DTOtoDomain);
                    await _repo.SaveAsync();

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return RedirectToAction("Details", new { id });
                }

                return View(vehicleMakeUpdateVM);
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
                VehicleMake model = await _repo.VehicleMake.GetVehicleMakeByIdAsync(id);

                if (model == null)
                {
                    return NotFound("NOT FOUND");
                }

                VehicleMakeVM DomainToVM = _mapper.Map<VehicleMakeVM>(model);

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
