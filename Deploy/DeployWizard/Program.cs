using System;
using System.Collections.Generic;
using System.Windows;
using Deploy.Lib.DeploymentProfiles;
using DeployWizard.Lib;
using DeployWizard.Lib.Controllers;
using DeployWizard.Lib.Steps;
using DeployWizard.Lib.Steps.Views;
using DeployWizard.Lib.Steps.Views.Wpf;
using DeployWizard.Lib.Views.Wpf;


namespace DeployWizard
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (RunningWindows())
            {
                var view = new WpfDeployWizardView();
                var model = new WizardModel();
                var steps = GetSteps(model);
                new DeployWizardController(model, view, steps);
                new Application().Run(view);
            }
        }

        private static IEnumerable<IWizardStep<IStepView>> GetSteps(WizardModel model)
        {
            var steps = new List<IWizardStep<IStepView>>();
            steps.Add(new SelectProfileStep(model, new WpfSelectProfileStepView(), ProfileManager.Instance));
            steps.Add(new SelectPathsStep(model, new WpfSelectPathsStepView()));
            return steps;
        }

        private static bool RunningWindows()
        {
            return true;
        }
    }
}
