using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{ CarId=1, BrandId=1, ColorId=1, CarName="Audi", ModelYear=1990, DailyPrice=300, Description="August Horch"},
                new Car{ CarId=2, BrandId=2, ColorId=2, CarName="Bugatti", ModelYear=1995, DailyPrice=500, Description="Ettore Bugatti"},
                new Car{ CarId=3, BrandId=3, ColorId=1, CarName="Buick", ModelYear=2000, DailyPrice=400, Description="David Dunbar Buick"},
                new Car{ CarId=4, BrandId=4, ColorId=2, CarName="Fiat", ModelYear=2005, DailyPrice=200, Description="Giovanni Agnelli"},
                new Car{ CarId=5, BrandId=5, ColorId=3, CarName="Ford", ModelYear=2010, DailyPrice=200, Description="Henry Ford"},
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c=>c.CarId==car.CarId);
            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car GetById(int carId)
        {
            Car carToGetById = _cars.SingleOrDefault(c => c.CarId == carId);
            return carToGetById;
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.CarName = car.CarName;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            
        }
    }
}
