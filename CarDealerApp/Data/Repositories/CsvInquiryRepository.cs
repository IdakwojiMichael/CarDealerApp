using CarDealerApp.Data.Models;

namespace CarDealerApp.Data.Repositories
{
    public class CsvInquiryRepository : IInquiryRepository
    {
        private readonly string _filePath = "Data/inquiries.csv";

        public void SaveInquiry(Inquiry inquiry)
        {
            var safeName = inquiry.Name.Replace(",", " ");
            var safeEmail = inquiry.Email.Replace(",", " ");
            var safeMessage = inquiry.Message.Replace(",", " ").Replace("\r", " ").Replace("\n", " ");
            var line = $"{safeName},{safeEmail},{safeMessage},{inquiry.CarId}";

            File.AppendAllLines(_filePath, new[] { line });
        }

        public List<Inquiry> GetAll()
        {
            var inquiries = new List<Inquiry>();

            if (!File.Exists(_filePath))
                return inquiries;

            var lines = File.ReadAllLines(_filePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');

                if (parts.Length < 4)
                    continue;

                try
                {
                    var inquiry = new Inquiry
                    {
                        Name = parts[0],
                        Email = parts[1],
                        Message = parts[2],
                        CarId = int.Parse(parts[3])
                    };

                    inquiries.Add(inquiry);
                }
                catch (FormatException)
                {
                    continue;
                }
            }

            return inquiries;
        }
    }
}








//using CarDealerApp.Data.Models;

//namespace CarDealerApp.Data.Repositories
//{
//    public class CsvInquiryRepository
//    {
//        private readonly string _filePath = "Data/inquiries.csv";

//        public void SaveInquiry(Inquiry inquiry)
//        {
//            var safeName = inquiry.Name.Replace(",", " ");
//            var safeEmail = inquiry.Email.Replace(",", " ");
//            var safeMessage = inquiry.Message.Replace(",", " ").Replace("\r", " ").Replace("\n", " ");
//            var line = $"{safeName},{safeEmail},{safeMessage},{inquiry.CarId}";

//            File.AppendAllLines(_filePath, new[] { line });
//        }


//        public List<Inquiry> GetAll()
//        {
//            var inquiries = new List<Inquiry>();

//            if (!File.Exists(_filePath))
//                return inquiries;

//            var lines = File.ReadAllLines(_filePath);

//            foreach (var line in lines)
//            {
//                if (string.IsNullOrWhiteSpace(line))
//                    continue;

//                var parts = line.Split(',');

//                if (parts.Length < 4)
//                    continue; // skip malformed lines

//                try
//                {
//                    var inquiry = new Inquiry
//                    {
//                        Name = parts[0],
//                        Email = parts[1],
//                        Message = parts[2],
//                        CarId = int.Parse(parts[3]) // ⚠️ this line was crashing
//                    };

//                    inquiries.Add(inquiry);
//                }
//                catch (FormatException)
//                {
//                    // Log or skip this bad line
//                    continue;
//                }
//            }

//            return inquiries;
//        }

//    }
//}
