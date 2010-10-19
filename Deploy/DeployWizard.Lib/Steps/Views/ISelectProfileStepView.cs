using System.Collections.Generic;
using DeployWizard.Lib.Events.Profile;

namespace DeployWizard.Lib.Steps.Views
{
    public interface ISelectProfileStepView : IStepView
    {
        event NewProfileEventHandler NewProfile;
        event DeleteProfileEventHandler DeleteProfile;
        IEnumerable<string> Profiles { get; set; }
        string SelectedProfile { get; set; }
    }

    public delegate void DeleteProfileEventHandler(object sender, ProfileEventHandlerArgs args);
    public delegate void NewProfileEventHandler(object sender, ProfileEventHandlerArgs args);
}
