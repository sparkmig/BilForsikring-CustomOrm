using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front
{
    public class PolicePage
    {
        public static void ShowAll(int carId)
        {
            var policeService = new PoliceService();

            while (true)
            {
                Console.Clear();
                var polices = policeService.GetPolices(carId);

                foreach (var police in polices)
                {
                    Console.WriteLine($"Police nummer: {police.PoliceNumber}");
                    Console.WriteLine($"Gyldig: {police.Start} - {police.End}");
                    Console.WriteLine($"Pris: {police.Price}");
                    Console.WriteLine();
                }

                Console.WriteLine("1) Tilføj Police");
                Console.WriteLine("2) Slet Police");

                string input = Console.ReadLine();

                if (input == "1")
                    Add(carId, policeService);
                else if (input == "2")
                    Delete(carId, policeService);
                else if (input == "-1")
                    return;
            }
        }
        public static void Add(int carId, PoliceService policeService)
        {
            Console.WriteLine("Start: ");
            DateTime start = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Slut: ");
            DateTime slut = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Pris: ");
            int price = int.Parse(Console.ReadLine());

            policeService.AddPolice(carId, start, slut, price);
        }
        public static void Delete(int carId, PoliceService policeService)
        {
            Console.Write("Policenummer: ");
            int policeNumber = int.Parse(Console.ReadLine());

            policeService.DeletePolice(policeNumber);
        }
    }
}
