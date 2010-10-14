using System.Linq;
using Deploy.Lib.DeploymentProfiles;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public class SelectProfileStep : WizardStepBase<ISelectProfileStepView>
    {
        private readonly IProfileManager _profileManager;

        public SelectProfileStep(WizardModel model, ISelectProfileStepView view, IProfileManager profileManager)
            : base(model, view)
        {
            _profileManager = profileManager;
            view.NewProfile += CreateNewProfile;
            var profiles = _profileManager.GetAll().Select(profile => profile.Name);
            View.Profiles = profiles;
            View.SelectedProfile = profiles.FirstOrDefault();
        }

        private void CreateNewProfile(object sender, NewProfileEventHandlerArgs args)
        {
            if (string.IsNullOrEmpty(args.ProfileName))
            {
                return;
            }
            _profileManager.Add(new DeploymentProfile { Name = args.ProfileName });
            var profiles = _profileManager.GetAll().Select(profile => profile.Name);
            View.Profiles = profiles;
            View.SelectedProfile = args.ProfileName;
        }
    }
}
