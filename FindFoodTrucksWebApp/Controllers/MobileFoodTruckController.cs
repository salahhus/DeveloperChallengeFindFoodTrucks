using FindFoodTrucksWebApp.Interfaces;
using FindFoodTrucksWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FindFoodTrucksWebApp.Controllers
{
    public class MobileFoodTruckController : Controller
    {
        private readonly IMobileFoodTruckService _mobileFoodTruckService;

        public MobileFoodTruckController(IMobileFoodTruckService mobileFoodTruckService)
        {
            this._mobileFoodTruckService = mobileFoodTruckService;    
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetFilteredMobileFoodTruck(InputModel inputModel)
        {
            var result = this._mobileFoodTruckService.GetMobileFoodFacilityPermitModelList(inputModel);
            if (result.IsSuccess)
            {
                return View(result.Data);  
            }
            return BadRequest(result.Data);
        }
    }
}
