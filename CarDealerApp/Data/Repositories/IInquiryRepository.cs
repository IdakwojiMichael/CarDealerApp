// IInquiryRepository.cs
using CarDealerApp.Data.Models;

namespace CarDealerApp.Data.Repositories
{
    public interface IInquiryRepository
    {
        List<Inquiry> GetAll();
        void SaveInquiry(Inquiry inquiry);
    }
}
