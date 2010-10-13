using DeployWizard.Components;

namespace DeployWizard
{
    public interface IWizardView
    {
        void ShowStep(ISetupStep setupStep);
        void SetNextEnabled(bool enabled);
        void SetFinishEnabled(bool enabled);
        void SetPreviousEnabled(bool enabled);
    }
}
