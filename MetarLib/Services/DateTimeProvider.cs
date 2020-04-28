using System;
using MetarLib.Contracts;

namespace MetarLib.Services
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now => DateTimeOffset.UtcNow;
    }
}