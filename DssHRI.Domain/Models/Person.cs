using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DssHRI.Domain.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }

        public Person() { }
    }
}
