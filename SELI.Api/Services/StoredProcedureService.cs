namespace SELI.Api.Services
{
    using Common.Models;
    using Domain.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;
    public class StoredProcedureService : IStoredProcedureService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StoredProcedureService> _logger;
        private readonly IDataContext _cnxSql;

        public StoredProcedureService(
            IConfiguration configuration,
            ILogger<StoredProcedureService> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _cnxSql = new DataContext(_configuration.GetConnectionString("SELIConnection"));
        }

        public async Task<int> AddNewSafePassage(SafePassage pass)
        {
            try
            {
                var i = await _cnxSql.NotifyNewSafePassage(pass);
                return i;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar nuevo salvoconducto");
                return 0;
            }
        }

        public async Task<Response> CancelSafePassage(int id)
        {
            try
            {
                if (await _cnxSql.CancelSafePassage(id))
                    return new Response()
                    {
                        IsSucess = true,
                        Message = "Salvoconducto cancelado!"
                    };
                else
                    return new Response()
                    {
                        IsSucess = false,
                        Message = "Error: No se pudo cancelar el salvoconducto"
                    };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar nuevo salvoconducto");
                return null;
            }
        }

        public async Task<SafePassage> GetSafePassage(int id)
        {
            try
            {
                var pass = await _cnxSql.GetSafePassage(id);
                return pass;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar nuevo salvoconducto");
                return null;
            }
        }
    }
}
