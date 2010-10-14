using System;
using DeployWizard.Lib.Steps;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Views
{
    public interface IDeployWizardView
    {
        event EventHandler PreviousClicked;
        event EventHandler NextClicked;
        event EventHandler FinishClicked;

        void SetPreviousEnabled(bool enabled);
        void SetNextEnabled(bool enabled);
        void SetFinishEnabled(bool enabled);
        void ShowStep(IWizardStep<IStepView> step);
    }
}