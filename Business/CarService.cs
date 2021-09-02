using Storage.Models;
using Storage.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CarService
    {
        public List<Car> GetCustomerCars(int customerId)
        {
            var repo = new CarRepository();
            return repo.Where(o => o.OwnerId == customerId).ToList();
        }

        public Car GetCar(int carId)
        {
            var repo = new CarRepository();
            return repo.FirstOrDefault(o => o.Id == carId);
        }

        public int AddCar(string make, string model, string year, string licenseplate, int ownerId)
        {
            var car = new Car()
            {
                Make = make,
                LicensePlate = licenseplate,
                Year = year,
                Model = model,
                OwnerId = ownerId,
            };
            var repo = new CarRepository();
            return repo.Insert(car);
        }
    }
}
