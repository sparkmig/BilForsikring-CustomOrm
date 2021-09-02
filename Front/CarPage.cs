using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front
{
    public class CarPage
    {
        public static void Show(int carId)
        {
            var carService = new CarService();
            while (true)
            {
                Console.Clear();
                var car = carService.GetCar(carId);

                Console.WriteLine(car.Id);
                Console.WriteLine("1) Accidents");
                Console.WriteLine("2) Polices");

                int.TryParse(Console.ReadLine(), out int input);
                if (input == -1)
                    return;
                else if (input == 1)
                    AccidentPage.ShowAll(carId);
                else if (input == 2)
                    PolicePage.ShowAll(carId);

            }
        }
    }
}
