using System;

namespace MetarLib.Contracts
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now { get; }
    }
}