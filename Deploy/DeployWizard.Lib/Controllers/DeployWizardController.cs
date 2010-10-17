using System;
using System.Collections.Generic;
using System.Linq;
using Deploy.Lib.Deployment.ProfileManagement;
using DeployWizard.Lib.Models;
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
        private readonly IProfileManager _profileManager;
        private readonly IWizardStep<IStepView> _finishStep;

        public DeployWizardController(WizardModel model, IDeployWizardView view, IProfileManager profileManager,
            IEnumerable<IWizardStep<IStepView>> steps, IWizardStep<IStepView> finishStep)
        {
            _model = model;
            model.ProfileChanged += ChangeTitle;
            _profileManager = profileManager;
            _view = view;
            _steps = steps;
            _finishStep = finishStep;
            _view.PreviousClicked += Previous;
            _view.NextClicked += Next;
            _view.SaveClicked += SaveProfile;
            _view.FinishClicked += Finish;
            _view.CloseClicked += Close;
            ShowCurrentStep();
        }

        private static void Close(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void SaveProfile(object sender, EventArgs e)
        {
            if (_model.CurrentProfile != null)
            {
                _profileManager.Save(_model.CurrentProfile);
            }
        }

        private void ChangeTitle(object sender, ProfileChangedEventArgs args)
        {
            _view.SetTitle("Deployer - " + args.Profile.Name);
        }

        private void UpdateButtonEnabling()
        {
            _view.SetPreviousEnabled(_currentIndex != 0);
            _view.SetNextEnabled(_currentIndex < (_steps.Count() - 1));
            _view.SetFinishEnabled(_currentIndex == (_steps.Count() - 1));
        }

        private void Finish(object sender, EventArgs e)
        {
            try
            {
                _finishStep.Prepare();
                _view.ShowStep(_finishStep);
                _finishStep.Validate();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex);
            }
            _view.PrepareToClose();
        }

        private void Next(object sender, EventArgs e)
        {
            try
            {
                CurrentStep().Validate();
            }
            catch (WizardStepException ex)
            {
                _view.ShowError(ex);
                return;
            }
            _currentIndex++;
            ShowCurrentStep();
        }

        private void Previous(object sender, EventArgs e)
        {
            _currentIndex--;
            ShowCurrentStep();
        }

        private void ShowCurrentStep()
        {
            UpdateButtonEnabling();
            var currentStep = CurrentStep();
            currentStep.Prepare();
            _view.ShowStep(currentStep);
        }

        private IWizardStep<IStepView> CurrentStep()
        {
            return _steps.ElementAt(_currentIndex);
        }
    }
}