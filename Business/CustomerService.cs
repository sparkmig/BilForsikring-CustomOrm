using Storage;
using Storage.Models;
using Storage.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class CustomerService
    {
        public List<Customer> GetCustomers()
        {
            var repo = new CustomerRepository();
            return repo.ToList();
        }

        public Customer GetCustomer(int id)
        {
            var repo = new CustomerRepository();
            return repo.FirstOrDefault(o => o.Id == id);
        }

        public int CreateCustomer()
        {
            var repo = new CustomerRepository();

            return repo.Insert(new Customer() { FirstName = "LisseLotte", EMail = "kurt@gmail.com",  LastName = "Jensen" });
        }
        public int RemoveCustomer(int id)
        {
            var repo = new CustomerRepository();
            return repo.Remove(new Customer() { Id = id });
        }
    }
}
