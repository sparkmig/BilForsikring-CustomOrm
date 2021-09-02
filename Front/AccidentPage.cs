using Business;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front
{
    public class AccidentPage
    {
        public static void ShowAll(int carId)
        {
            var accidentService = new AccidentService();

            while (true)
            {
                Console.Clear();
                var accidents = accidentService.GetAccidents(carId);

                foreach (var accident in accidents)
                {
                    Console.WriteLine($"{accident.Id}) {accident.Date}");
                    Console.WriteLine($"Beskrivelse: {accident.Description}");
                    Console.WriteLine($"Pris: {accident.Price}");
                    Console.WriteLine();
                }

                Console.WriteLine("--------------------");
                Console.WriteLine("1) Tilføj");
                Console.WriteLine("2) Slet");

                int.TryParse(Console.ReadLine(), out int input);

                if (input == -1)
                    return;
                else if (input == 2)
                    Delete(accidents, accidentService);
                else if (input == 1)
                    Add(carId, accidentService);
            }
        }
        public static void Delete(List<Accident> accidents, AccidentService accidentService)
        {
            Console.WriteLine("Nummer på den du ville slette: ");
            int.TryParse(Console.ReadLine(), out int result);
            while (!accidents.Any(o => o.Id == result))
            {
                Console.WriteLine("Nummer på den du ville slette: ");
                int.TryParse(Console.ReadLine(), out result);
                if (result == -1)
                    return;
            }
            Console.WriteLine("Sletter...");
            accidentService.DeleteAccident(result);
            Console.WriteLine("Slettet!");
        }
        public static void Add(int carId, AccidentService accidentService)
        {
            Console.WriteLine("Beskrivelse:");
            string description = Console.ReadLine();

            Console.WriteLine("Dato: ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Samlet Pris: ");
            int price = int.Parse(Console.ReadLine());

            Console.WriteLine("Tilføjer...");
            accidentService.AddAccident(description, price, date, carId);
            Console.WriteLine("Tilføjet!");
        }
    }
}
