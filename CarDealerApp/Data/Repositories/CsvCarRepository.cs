using CarDealerApp.Data.Models;
using System.Globalization;

namespace CarDealerApp.Data.Repositories
{
    public class CsvCarRepository : ICarRepository
    {
        private readonly string _filePath = "Data/cars.csv";

        public List<Car> GetAll()
        {
            if (!File.Exists(_filePath)) return new List<Car>();

            return File.ReadAllLines(_filePath)
                .Skip(1)
                .Select(line => line.Split(','))
                .Select(values => new Car
                {
                    Id = int.Parse(values[0]),
                    Make = values[1],
                    Model = values[2],
                    Year = int.Parse(values[3]),
                    Price = decimal.Parse(values[4], CultureInfo.InvariantCulture),
                    ImageUrl = values[5]
                }).ToList();
        }

        public Car? GetById(int id)
        {
            return GetAll().FirstOrDefault(c => c.Id == id);
        }

        public void Add(Car car)
        {
            var cars = GetAll();
            car.Id = cars.Any() ? cars.Max(c => c.Id) + 1 : 1;

            var newLine = $"{car.Id},{car.Make},{car.Model},{car.Year},{car.Price.ToString(CultureInfo.InvariantCulture)},{car.ImageUrl}";

            // If file is new, add header
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "Id,Make,Model,Year,Price,ImageUrl\n");
            }

            using var writer = File.AppendText(_filePath);
            writer.WriteLine(newLine);
        }
    }
}











//using CarDealerApp.Data.Models;
//using System.Globalization;

//namespace CarDealerApp.Data.Repositories
//{
//    public class CsvCarRepository
//    {
//        private readonly string _filePath = "Data/cars.csv";

//        public List<Car> GetAll()
//        {
//            if (!File.Exists(_filePath)) return new List<Car>();

//            return File.ReadAllLines(_filePath)
//                .Skip(1)
//                .Select(line => line.Split(','))
//                .Select(values => new Car
//                {
//                    Id = int.Parse(values[0]),
//                    Make = values[1],
//                    Model = values[2],
//                    Year = int.Parse(values[3]),
//                    Price = decimal.Parse(values[4], CultureInfo.InvariantCulture),
//                    ImageUrl = values[5]
//                }).ToList();
//        }

//        public Car? GetById(int id)
//        {
//            return GetAll().FirstOrDefault(c => c.Id == id);
//        }

//        public void Add(Car car)
//        {
//            var cars = GetAll();
//            car.Id = cars.Any() ? cars.Max(c => c.Id) + 1 : 1;
//            using var writer = File.AppendText(_filePath);
//            writer.WriteLine($"{car.Id},{car.Make},{car.Model},{car.Year},{car.Price.ToString(CultureInfo.InvariantCulture)},{car.ImageUrl}");
//        }
//    }
//}
