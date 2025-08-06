using CarDealerApp.Data.Models;
using CarDealerApp.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly CsvCarRepository _carRepo;
        private readonly CsvInquiryRepository _inquiryRepo;

        public HomeController(CsvCarRepository carRepo, CsvInquiryRepository inquiryRepo)
        {
            _carRepo = carRepo;
            _inquiryRepo = inquiryRepo;
        }

        public IActionResult Index()
        {
            var cars = _carRepo.GetAll();
            return View(cars);
        }

        public IActionResult Details(int id)
        {
            var car = _carRepo.GetById(id);
            if (car == null)
                return NotFound();

            return View(car);
        }

        [HttpPost]
        public IActionResult SubmitInquiry(Inquiry inquiry)
        {
            if (ModelState.IsValid)
            {
                _inquiryRepo.SaveInquiry(inquiry); 
                TempData["SuccessMessage"] = "Inquiry submitted successfully!";
                return RedirectToAction("Index");
            }

            var car = _carRepo.GetById(inquiry.CarId);
            return View("Details", car);
        }

    }
}
