using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SELI.Api.Services;
using SELI.Common.Models;
using System;
using System.Threading.Tasks;

namespace SELI.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class SafePassagesController : ControllerBase
    {
        private readonly IStoredProcedureService _spService;
        public SafePassagesController(IStoredProcedureService spService)
        {
            _spService = spService;
        }


        /// <summary>
        /// Obtener salvoconducto por código
        /// </summary>
        /// <param name="id">Número de identificador del salvoconducto</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id == default) return BadRequest("Código no válido");
            try
            {
                var safePassage = await _spService.GetSafePassage(id);
                return Ok(safePassage);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }

        }

        /// <summary>
        /// Crear un nuevo salvoconducto oficial
        /// </summary>
        /// <param name="model">Objeto de tipo salvoconducto</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddNewSafePassage(SafePassage model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Forbid();
                var response = await _spService.AddNewSafePassage(model);
                if (response == default) return BadRequest();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Anular salvoconducto 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == default) return BadRequest("Código no válido");
            try
            {
                Response cancelRequest = await _spService.CancelSafePassage(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
