using System.Collections.Generic;

namespace DeployWizard.Wpf.Steps.Views
{
    public class AutoCompleteSuggestions
    {
        public IEnumerable<string> Suggestions { get; set; }

        public AutoCompleteSuggestions()
        {
            Suggestions = new string[0];
        }
    }
}
