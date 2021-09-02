using Storage.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Models
{
    [Table("Polices")]
    public class Police
    {
        public int PoliceNumber { get; set; }
        public int CarId { get; set; }
        public int Price { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
