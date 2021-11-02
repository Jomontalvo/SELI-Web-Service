using SELI.Common.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SELI.Domain.Helpers
{
    public class DataContext : Initialize<string>, IDataContext
    {
        private readonly SqlConnection _cnx;

        public DataContext(string connectionString) : base(connectionString)
        {
            _cnx = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Obtiene los detalles de un salvoconducto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SafePassage> GetSafePassage(int id)
        {
            SafePassage pass = null;
            try
            {
                using (_cnx)
                {
                    using SqlCommand cmd = new("PE_C_SELI_ConsultarSalvoconducto", _cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Opcion", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@IdSalvoconducto", SqlDbType.Int).Value = id;
                    await _cnx.OpenAsync();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                    pass = await ConvertReaderInSafePassage(reader);
                }
            }
            catch (Exception)
            {
                if (_cnx != null && _cnx.State == ConnectionState.Open)
                {
                    _cnx.Close();
                }
            }
            return pass;
        }

        /// <summary>
        /// Convertir el registro de lectura SqlReader en un Objeto Salvoconducto
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static async Task<SafePassage> ConvertReaderInSafePassage(SqlDataReader reader)
        {
            SafePassage resultPass = new();
            while (await reader.ReadAsync())
            {
                resultPass.Id = Convert.ToInt32(reader["IdSalvoconducto"]);
                resultPass.CodeId = Convert.ToInt32(reader["NumeroSalvoconducto"]);
                resultPass.EmissionDate = Convert.ToDateTime(reader["FechaRegistro"]);
                resultPass.DestinationPlace = reader["LugarMision"].ToString();
                resultPass.Reasons = reader["MotivoMision"].ToString();
            }
            return resultPass;
        }

        /// <summary>
        /// Notificar la expedición de nuevo salvoconducto y crearlo en la Base de Datos
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> NotifyNewSafePassage(SafePassage model)
        {
            int newId = default;
            try
            {
                using (_cnx)
                {
                    using SqlCommand cmd = new("PE_C_SELI_GuardarSalvoconducto", _cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@p1", SqlDbType.VarChar, 255).Value = "ADMIN";
                    cmd.Parameters.Add("@p2", SqlDbType.VarChar, 255).Value = string.Empty;
                    // Add all parameters
                    await _cnx.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception)
            {
                if (_cnx != null && _cnx.State == ConnectionState.Open)
                {
                    _cnx.Close();
                }
            }
            return newId;
        }

        /// <summary>
        /// Cancelar salvoconducto expedido y vigente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CancelSafePassage(int id)
        {
            try
            {
                using (_cnx)
                {
                    using SqlCommand cmd = new("PE_C_SELI_AnularSalvoconducto", _cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdSalvoconducto", SqlDbType.Int).Value = id;

                    // Add all parameters
                    await _cnx.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                if (_cnx != null && _cnx.State == ConnectionState.Open)
                {
                    _cnx.Close();
                }
                return false;
            }
        }

        /// <summary>
        /// Verificación de Inicio de sesión
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<Response> LoginAsync(User userModel)
        {
            var userLogged  = new Response()
            {
                IsSucess = true,
                Message = "Success login.",
                Result = new UserDTO { UserName = "juan@gmail.com", Password="ASFDG2353426346==", Role = "Suscriptor"}
            };
            return await Task.FromResult(userLogged);
        }
    }
}
