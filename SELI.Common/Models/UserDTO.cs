using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELI.Common.Models
{
    /// <summary>
    /// Objeto de transferencia de datos de usuario
    /// </summary>
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
