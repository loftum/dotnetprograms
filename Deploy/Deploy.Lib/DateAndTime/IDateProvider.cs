using System;

namespace Deploy.Lib.DateAndTime
{
    public interface IDateProvider
    {
        DateTime Now();
    }
}