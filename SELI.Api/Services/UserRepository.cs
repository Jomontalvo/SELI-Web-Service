using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SELI.Common.Models;
using SELI.Domain.Helpers;
using System;
using System.Threading.Tasks;

namespace SELI.Api.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StoredProcedureService> _logger;
        private readonly IDataContext _cnxSql;

        public UserRepository(IConfiguration configuration,
            ILogger<StoredProcedureService> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _cnxSql = new DataContext(_configuration.GetConnectionString("SELIConnection"));
        }

        /// <summary>
        /// Obtener detalles del usuario autenticado
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<UserDTO> GetUser(User userModel)
        {
            try
            {
                Response userResponse = await _cnxSql.LoginAsync(userModel);
                if (userResponse.IsSucess)
                {
                    UserDTO userAuthenticated = (UserDTO)userResponse.Result;
                    return userAuthenticated;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retornar Response de Login de usuario
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<Response> Login(User userModel)
        {
            return await _cnxSql.LoginAsync(userModel);
        }
    }
}
