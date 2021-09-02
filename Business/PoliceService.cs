using Storage.Models;
using Storage.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PoliceService
    {
        public List<Police> GetPolices(int carId) => new PoliceRepository().Where(o => o.CarId == carId).ToList();
        public int DeletePolice(int policenumber) => new PoliceRepository().Remove(new Police() { PoliceNumber = policenumber });
        public int AddPolice(int carId, DateTime start, DateTime end, int price)
        {
            Police police = new Police()
            {
                CarId = carId,
                End = end,
                Price = price,
                Start = start
            };

            return new PoliceRepository().Insert(police);
        }
    }
}
