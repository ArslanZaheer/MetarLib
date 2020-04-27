using System;
using MetarLib.Contracts;

namespace MetarLib
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now => DateTimeOffset.UtcNow;
    }
}