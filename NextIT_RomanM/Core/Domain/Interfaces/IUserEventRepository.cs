using NextIT_RomanM.Core.Domain.Models;

namespace NextIT_RomanM.Core.Domain.Interfaces
{
    public interface IUserEventRepository
    {
        Task SaveBatch(IEnumerable<UserEvent> userEvents);
    }
}
