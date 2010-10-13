using System;
using System.Collections.Generic;
using System.Linq;
using DeployWizard.Lib.Steps;
using DeployWizard.Lib.Views;

namespace DeployWizard.Lib.Controllers
{
    public class DeployWizardController
    {
        private readonly IDeployWizardView _view;
        private readonly IEnumerable<IWizardStep> _steps;
        private int _currentIndex;

        public DeployWizardController(IDeployWizardView view, IEnumerable<IWizardStep> steps)
        {
            _view = view;
            _steps = steps;
            _view.PreviousClicked += Previous;
            _view.NextClicked += Next;
            _view.FinishClicked += Finish;
            UpdateButtonEnabling();
        }

        private void UpdateButtonEnabling()
        {
            _view.SetPreviousEnabled(_currentIndex != 0);
            _view.SetNextEnabled(_currentIndex <= (_steps.Count() - 1));
            _view.SetFinishEnabled(_currentIndex == (_steps.Count() - 1));
        }

        private void Finish(object sender, EventArgs e)
        {   

        }

        private void Next(object sender, EventArgs e)
        {
            _currentIndex++;
        }

        private void Previous(object sender, EventArgs e)
        {
            _currentIndex--;
        }
    }
}