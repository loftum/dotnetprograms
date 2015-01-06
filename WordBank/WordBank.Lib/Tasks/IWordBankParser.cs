using System.Collections.Generic;

namespace WordBank.Lib.Tasks
{
    public interface IWordBankParser
    {
        T Parse<T>(IList<string> values) where T : class, new();
    }
}