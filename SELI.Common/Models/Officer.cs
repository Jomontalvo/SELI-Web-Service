using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELI.Common.Models
{
    public class Officer : Person
    {
        public string Position { get; set; }
        public Entity Institution { get; set; }
    }
}
