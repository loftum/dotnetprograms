using System;

namespace StuffLibrary.Common.DateAndTime
{
    public interface IDateProvider
    {
        DateTime Now { get; }
    }
}