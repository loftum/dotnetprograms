using System.Collections.Generic;

namespace DeployWizard.Lib.AutoComplete
{
    public interface IAutoCompleteProvider
    {
        IEnumerable<string> GetSuggestionsFor(string input);
    }
}