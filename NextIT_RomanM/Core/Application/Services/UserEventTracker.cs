using Microsoft.AspNetCore.Authentication;
using NextIT_RomanM.Core.Domain.Models;
using System.Collections.Concurrent;

namespace NextIT_RomanM.Core.Application.Services
{
    public class UserEventTracker
    {
        private readonly ConcurrentQueue<UserEvent> _queue;
        private readonly ISystemClock _systemClock;

        public UserEventTracker(ConcurrentQueue<UserEvent> queue, ISystemClock systemClock)
        {
            _queue = queue;
            _systemClock = systemClock;
        }

        public void TrackEvent(string eventType, params KeyValuePair<string, object>[] param)
        {
            UserEvent userEvent = new()
            {
                // NOTICE: Here we will get the userId from a repository (if we were using EF... from the scope)
                Username = "admin",
                Params = param.ToList(),
                RequiredAt = _systemClock.UtcNow
            };

            _queue.Enqueue(userEvent);
        }

        public IEnumerable<UserEvent> Emit(int emitCount)
        {
            Console.WriteLine("Saving...");


            if (_queue.IsEmpty) return Enumerable.Empty<UserEvent>();

            List<UserEvent> toExport = new();
            while(toExport.Count < emitCount && !_queue.IsEmpty)
            {
                if(_queue.TryDequeue(out var userEvent))
                {
                    toExport.Add(userEvent);
                }
            }

            return toExport;
        }
    }
}
