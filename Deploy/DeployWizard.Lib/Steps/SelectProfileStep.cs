using System.Collections.Generic;
using System.Linq;
using Deploy.Lib.Deployment.ProfileManagement;
using Deploy.Lib.Deployment.Profiles;
using DeployWizard.Lib.Events.Profile;
using DeployWizard.Lib.Models;
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
            View.NewProfile += CreateNewProfile;
            View.DeleteProfile += DeleteProfile;
        }

        private void DeleteProfile(object sender, ProfileEventHandlerArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.ProfileName))
            {
                return;
            }
            _profileManager.Delete(args.ProfileName);
            UpdateProfilesInView();
            View.SelectedProfile = View.Profiles.FirstOrDefault();
        }

        private void CreateNewProfile(object sender, ProfileEventHandlerArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.ProfileName))
            {
                return;
            }
            _profileManager.Add(new DeploymentProfile { Name = args.ProfileName });
            UpdateProfilesInView();
            View.SelectedProfile = args.ProfileName;
        }

        private void UpdateProfilesInView()
        {
            var profiles = _profileManager.GetAll().Select(profile => profile.Name);
            View.Profiles = profiles;
        }

        protected override void DoValidate()
        {
            var currentProfileName = View.SelectedProfile;
            if (string.IsNullOrWhiteSpace(currentProfileName))
            {
                throw new WizardStepException("No profile is selected");
            }
            Model.CurrentProfile = _profileManager.Get(View.SelectedProfile);
        }

        public override void Prepare()
        {
            var profiles = _profileManager.GetAll().Select(profile => profile.Name);
            View.Profiles = profiles;
            View.SelectedProfile = Model.CurrentProfile == null? profiles.FirstOrDefault() : Model.CurrentProfile.Name;
        }
    }
}
