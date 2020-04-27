using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.AssertHelpers
{
    public static class AssertExtensions
    {
        public static void EnumHasFlag<TEnum>(this Assert assert, TEnum expectedFlag, TEnum enumValue)
            where TEnum : Enum
        {
            if (!enumValue.HasFlag(expectedFlag))
                Assert.Fail("Enum value does not contain flag '{0}'", expectedFlag);
        }
    }
}