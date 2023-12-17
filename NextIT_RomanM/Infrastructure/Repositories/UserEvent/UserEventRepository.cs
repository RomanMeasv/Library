using NextIT_RomanM.Core.Domain.Interfaces;

namespace NextIT_RomanM.Infrastructure.Repositories.UserEvent
{
    public class UserEventRepository : IUserEventRepository
    {
        public Task SaveBatch(IEnumerable<Core.Domain.Models.UserEvent> userEvents)
        {
            return Task.CompletedTask;
        }
    }
}
