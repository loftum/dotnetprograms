using System.Collections.Generic;

namespace DeployWizard.Lib.Steps.Views
{
    public interface ISelectProfileStepView : IStepView
    {
        event NewProfileEventHandler NewProfile;
        IEnumerable<string> Profiles { get; set; }
        string SelectedProfile { get; set; }
    }

    public delegate void NewProfileEventHandler(object sender, NewProfileEventHandlerArgs args);

    public class NewProfileEventHandlerArgs
    {
        public string ProfileName { get; private set; }

        public NewProfileEventHandlerArgs(string profileName)
        {
            ProfileName = profileName;
        }
    }
}
