using Storage.Models;
using Storage.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class AccidentService
    {
        public List<Accident> GetAccidents(int carId)
        {
            var repo = new AccidentRepository();
            return repo.Where(o => o.CarId == carId).ToList();
        }
        public int DeleteAccident(int id)
        {
            var repo = new AccidentRepository();
            return repo.Remove(new Accident() { Id = id });
        }
        public int AddAccident(string description, int price, DateTime date, int carId)
        {
            var accident = new Accident()
            {
                CarId = carId,
                Date = date,
                Description = description,
                Price = price,
            };

            var repo = new AccidentRepository();
            return repo.Insert(accident);
        }
    }
}
