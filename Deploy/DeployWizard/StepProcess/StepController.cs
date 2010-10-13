using System;
using System.Collections.Generic;
using DeployWizard.Components;

namespace DeployWizard.StepProcess
{
    public class StepController
    {
        private readonly IList<ISetupStep> _setupSteps = new List<ISetupStep>();
        private readonly IWizardView _view;
        private int _currentIndex;
        private readonly ISetupStep _finished = new Finished();

        public StepController(IWizardView view)
        {
            _view = view;
            _setupSteps.Add(new Welcome());
            _setupSteps.Add(new FirstStep());
            _setupSteps.Add(new LastStep());
            _view.ShowStep(_setupSteps[0]);
            _view.SetPreviousEnabled(false);
            _view.SetFinishEnabled(false);
            _view.SetNextEnabled(true);
        }

        public void Next()
        {
            _currentIndex++;
            if (_currentIndex == (_setupSteps.Count - 1))
            {
                _view.SetPreviousEnabled(true);
                _view.SetNextEnabled(false);
                _view.SetFinishEnabled(true);
            }
            _view.ShowStep(_setupSteps[_currentIndex]);
        }


        public void Previous()
        {
            _currentIndex--;
            if (_currentIndex == 0)
            {
                _view.SetPreviousEnabled(false);
            }
            _view.SetNextEnabled(true);
            _view.SetFinishEnabled(false);
            _view.ShowStep(_setupSteps[_currentIndex]);
        }

        public void Finish()
        {
            _view.ShowStep(_finished);
        }
    }
}
