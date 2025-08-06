using CarDealerApp.Data.Models;
using System;

namespace CarDealerApp.Data.Repositories
{
    public interface ICarRepository
    {
        List<Car> GetAll();
        Car GetById(int id);
        void Add(Car car);
    }
}