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
        event EventHandler CloseClicked;
        event EventHandler SaveClicked;
        event EventHandler FastForwardClicked;

        void SetPreviousEnabled(bool enabled);
        void SetNextEnabled(bool enabled);
        void SetFastForwardEnabled(bool enabled);
        void SetFinishEnabled(bool enabled);
        void ShowStep(IWizardStep<IStepView> step);
        void ShowError(Exception exception);
        void SetTitle(string title);
        void PrepareToClose();
    }
}