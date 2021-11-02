namespace SELI.Api.Services
{
    using Common.Models;
    using System.Threading.Tasks;

    public interface IStoredProcedureService
    {
        Task<int> AddNewSafePassage(SafePassage pass);
        Task<SafePassage> GetSafePassage(int id);
        Task<Response> CancelSafePassage(int id);
        Task<Response> LoginAsync(User userModel);
    }
}