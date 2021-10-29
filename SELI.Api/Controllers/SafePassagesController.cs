using Microsoft.AspNetCore.Mvc;
using SELI.Api.Services;
using SELI.Common.Models;
using System;
using System.Threading.Tasks;

namespace SELI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        /// <param name="id"></param>
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
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
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id)
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
