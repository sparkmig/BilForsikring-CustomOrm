using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repos
{
    public class CustomerRepository : ORM<Customer>
    {
        public CustomerRepository() : 
            base("Server=localhost;Database=forsikring;User Id=sa;Password=1234;") 
        { }
    }
}
