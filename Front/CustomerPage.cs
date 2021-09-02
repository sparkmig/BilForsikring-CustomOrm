using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front
{
    public class CustomerPage
    {
        public static void Show(int customerId)
        {
            var customerService = new CustomerService();
            var carSerivce = new CarService();
            while (true)
            {
                Console.Clear();
                var customer = customerService.GetCustomer(customerId);
                var cars = carSerivce.GetCustomerCars(customerId);

                Console.WriteLine($"{customer.FirstName} {customer.LastName}:");
                Console.WriteLine("Biler");

                foreach (var car in cars)
                {
                    Console.WriteLine($"{car.Id}) {car.LicensePlate} ({car.Make} {car.Model})");
                }
                Console.WriteLine($"0) Tilføj bil");
                int.TryParse(Console.ReadLine(), out int input);
                if (input == -1)
                    return;
                else if (cars.Any(o => o.Id == input))
                    CarPage.Show(input);
                else if (input == 0)
                {
                    AddCar(customerId);
                }
            }
        }
        public static void AddCar(int customerId)
        {
            Console.Write("Mærke: ");
            string make = Console.ReadLine();

            Console.Write("Model: ");
            string model = Console.ReadLine();

            Console.Write("År: ");
            string year = Console.ReadLine();

            Console.Write("Nummerplade: ");
            string licenseplate = Console.ReadLine();

            new CarService().AddCar(make, model, year, licenseplate, customerId);
        }
    }
}
