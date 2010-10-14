using System;
using System.Collections.Generic;
using System.Linq;
using DeployWizard.Lib.Steps;
using DeployWizard.Lib.Steps.Views;
using DeployWizard.Lib.Views;

namespace DeployWizard.Lib.Controllers
{
    public class DeployWizardController
    {
        private readonly IDeployWizardView _view;
        private readonly IEnumerable<IWizardStep<IStepView>> _steps;
        private readonly WizardModel _model;
        private int _currentIndex;

        public DeployWizardController(WizardModel model, IDeployWizardView view, IEnumerable<IWizardStep<IStepView>> steps)
        {
            _model = model;
            _view = view;
            _steps = steps;
            _view.PreviousClicked += Previous;
            _view.NextClicked += Next;
            _view.FinishClicked += Finish;
            UpdateButtonEnabling();
            _view.ShowStep(_steps.ElementAt(0));
        }

        private void UpdateButtonEnabling()
        {
            _view.SetPreviousEnabled(_currentIndex != 0);
            _view.SetNextEnabled(_currentIndex < (_steps.Count() - 1));
            _view.SetFinishEnabled(_currentIndex == (_steps.Count() - 1));
        }

        private void Finish(object sender, EventArgs e)
        {   

        }

        private void Next(object sender, EventArgs e)
        {
            _currentIndex++;
            UpdateButtonEnabling();
            _view.ShowStep(_steps.ElementAt(_currentIndex));
        }

        private void Previous(object sender, EventArgs e)
        {
            _currentIndex--;
            UpdateButtonEnabling();
            _view.ShowStep(_steps.ElementAt(_currentIndex));
        }
    }
}