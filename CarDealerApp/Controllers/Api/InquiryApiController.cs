using Microsoft.AspNetCore.Mvc;
using CarDealerApp.Data.Models;
using CarDealerApp.Data.Repositories;

namespace CarDealerApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquiryApiController : ControllerBase
    {
        private readonly IInquiryRepository _inquiryRepo;

        public InquiryApiController(IInquiryRepository inquiryRepo)
        {
            _inquiryRepo = inquiryRepo;
        }

        [HttpPost]
        public IActionResult SubmitInquiry([FromBody] Inquiry inquiry)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _inquiryRepo.SaveInquiry(inquiry);
            return Ok(new { message = "Inquiry submitted successfully." });
        }
    }
}
