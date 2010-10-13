using System;

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
    }
}