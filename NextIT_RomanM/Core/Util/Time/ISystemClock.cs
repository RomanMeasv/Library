namespace NextIT_RomanM.Core.Util.Time
{
    public interface ISystemClock
    {
        public DateTimeOffset UtcNow { get; }
        public DateTimeOffset Now { get; }
    }
}
