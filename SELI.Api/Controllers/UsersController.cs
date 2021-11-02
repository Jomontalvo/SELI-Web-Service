using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SELI.Api.Services;
using SELI.Common.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SELI.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private string generatedToken = string.Empty;

        public UsersController(IConfiguration config,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Echo para verificar disponibilidad del servicio de autenticación
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("echoping")]
        public IActionResult EchoPing()
        {
            return Ok(true);
        }


        /// <summary>
        /// Obtener información de un usuario autenticado.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("info")]
        [Produces(typeof(UserDTO))]
        public async Task<IActionResult> PostUser([FromBody] User credentials)
        {
            UserDTO spResponse = await _userRepository.GetUser(credentials);
            if (spResponse == null)
            {
                return BadRequest("Error en el inicio de sesión. Verifique usuario y contraseña");
            }
            return Ok(spResponse);
        }

        /// <summary>
        /// Iniciar sesión para obtener token
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> PostAuthenticate([FromBody] User credentials)
        {
            if ((credentials == null)
                || (string.IsNullOrEmpty(credentials.UserName))
                || (string.IsNullOrEmpty(credentials.Password)))
                return BadRequest("Error: credenciales incompletas.");

            var loginResponse = await _userRepository.Login(credentials);

            if (!loginResponse.IsSucess)
            {
                return Unauthorized();
            }

            UserDTO validUser = (UserDTO)loginResponse.Result;
            generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(),
                validUser);

            if (generatedToken != null)
            {
                return Ok(generatedToken);
            }
            else
            {
                return BadRequest("Error al generar Token con credenciales ingresadas.");
            }
        }
    }
}
