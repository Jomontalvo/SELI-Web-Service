using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELI.Common.Models
{
    public class Entity
    {
        [Required]
        public char IdArea { get; set; }
        public string IdEntity { get; set; }
        public string IdDependency { get; set; }
        public string EntityCode => $"{IdArea}{IdEntity}{IdDependency}";
        public string Name { get; set; }
        public string Acronym { get; set; }
        public String Address { get; set; }
        public Tuple<int,string> Department { get; set; }
    }
}
