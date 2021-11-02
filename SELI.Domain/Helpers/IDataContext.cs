using SELI.Common.Models;
using System.Threading.Tasks;

namespace SELI.Domain.Helpers
{
    public interface IDataContext
    {
        Task<int> NotifyNewSafePassage(SafePassage pass);
        Task<SafePassage> GetSafePassage(int id);
        Task<bool> CancelSafePassage(int id);
        Task<Response> LoginAsync(User userModel);
    }
}
