using System;
using System.Collections.Generic;
using System.Windows;
using Deploy.Lib.Deployment.ProfileManagement;
using Deploy.Lib.FileManagement;
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
                new DeployWizardController(model, view, ProfileManager.Instance, steps);
                new Application().Run(view);
            }
        }

        private static IEnumerable<IWizardStep<IStepView>> GetSteps(WizardModel model)
        {
            var fileSystemManager = new FileSystemManager();
            var steps = new List<IWizardStep<IStepView>>();
            steps.Add(new SelectProfileStep(model, new WpfSelectProfileStepView(), ProfileManager.Instance));
            steps.Add(new SetUpBackupStep(model, new WpfSetUpBackupStepView(), fileSystemManager));
            steps.Add(new SetUpDeployStatusStep(model, new WpfSetUpDeployStatusStepView(), fileSystemManager));
            steps.Add(new SelectPackageStep(model, new WpfSelectPackageStepView(), fileSystemManager));
            return steps;
        }

        private static bool RunningWindows()
        {
            return true;
        }
    }
}
