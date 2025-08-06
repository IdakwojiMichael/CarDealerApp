using Microsoft.AspNetCore.Mvc;
using CarDealerApp.Data.Repositories;

namespace CarDealerApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarApiController : ControllerBase
    {
        private readonly ICarRepository _carRepo;

        public CarApiController(ICarRepository carRepo)
        {
            _carRepo = carRepo;
        }

        [HttpGet]
        public IActionResult GetAllCars()
        {
            var cars = _carRepo.GetAll();
            return Ok(cars);
        }
    }
}
