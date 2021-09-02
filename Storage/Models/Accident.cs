using Storage.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Models
{
    [Table("Accidents")]
    public class Accident
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
    }
}
