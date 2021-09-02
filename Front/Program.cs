using Business;
using System;
using System.Linq;

namespace Front
{
    class Program
    {
        static void Main(string[] args)
        {
            var customerService = new CustomerService();

            while (true)
            {
                Console.Clear();
                var customers = customerService.GetCustomers();
                Console.WriteLine("Vælg kunde:");
                for (int i = 0; i < customers.Count; i++)
                {
                    var c = customers[i];
                    Console.WriteLine($"{c.Id}) {c.FirstName} {c.LastName}");
                }
                
                int.TryParse(Console.ReadLine(), out int input);
                
                if (customers.Any(o => o.Id == input))
                    CustomerPage.Show(input);

            }
        }
    }
}
