using CarDealerApp.Data.Models;
using CarDealerApp.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly CsvCarRepository _carRepository;
        private readonly CsvInquiryRepository _inquiryRepository;
        private readonly IWebHostEnvironment _environment;

        public AdminController(CsvCarRepository carRepository, CsvInquiryRepository inquiryRepository, IWebHostEnvironment environment)
        {
            _carRepository = carRepository;
            _inquiryRepository = inquiryRepository;
            _environment = environment;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                HttpContext.Session.SetString("Admin", "true");
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid credentials.";
            return View();
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
                return RedirectToAction("Login");

            return View(new Car());
        }

        [HttpPost]
        public IActionResult Dashboard(Car car, IFormFile carImage)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
                return RedirectToAction("Login");

            if (ModelState.IsValid && carImage != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(carImage.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    carImage.CopyTo(stream);
                }

                car.ImageUrl = "/uploads/" + fileName;
                _carRepository.Add(car);

                TempData["Success"] = "Car added!";
                return RedirectToAction("Dashboard");
            }

            return View(car);
        }

        public IActionResult Inquiries()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
                return RedirectToAction("Login");

            var inquiries = _inquiryRepository.GetAll();
            return View(inquiries);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login");
        }
    }
}
