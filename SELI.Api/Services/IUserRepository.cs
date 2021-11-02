using SELI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SELI.Api.Services
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUser(User userModel);
        Task<Response> Login(User userModel);
    }
}
