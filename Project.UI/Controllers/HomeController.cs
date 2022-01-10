using Microsoft.AspNetCore.Mvc;
using Project.Repository.Common.Interfaces;
using Project.Repository.Common.Interfaces.UOW;
using Project.Service.Common;
using System.Threading.Tasks;

namespace Project.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicesWrapper _servicesWrapper;

        public HomeController(IServicesWrapper servicesWrapper)
        {
            _servicesWrapper = servicesWrapper;
        }

        public async Task<IActionResult> Index()
        {
            string tempCarMake = "Acuraa";
            if (await _servicesWrapper.VehicleMake.FindIfExists(x => x.Name == tempCarMake))
            {
                return Content($"{tempCarMake} exists");
            }
            return View();
        }

        public async Task<IActionResult> TestRefLoop()
        {
            var result = await _servicesWrapper.VehicleModel.GetVehicleModelByIdAsync(1);
            return Ok(result);
        }

    }
}
