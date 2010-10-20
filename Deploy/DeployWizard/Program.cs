using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using Deploy.Lib.Deployment.ProfileManagement;
using Deploy.Lib.FileManagement;
using DeployWizard.Lib.AutoComplete.FileSystem;
using DeployWizard.Lib.Controllers;
using DeployWizard.Lib.Models;
using DeployWizard.Lib.Steps;
using DeployWizard.Lib.Steps.Views;
using DeployWizard.Wpf.Steps.Views;
using DeployWizard.Wpf.Views;


namespace DeployWizard
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            PrintPlatformInfo();
            if (RunningWindows())
            {
                var view = new WpfDeployWizardView();
                var model = new WizardModel();
                var steps = GetSteps(model);
                var finishStep = GetFinishStep(model);
                new DeployWizardController(model, view, ProfileManager.Instance, steps, finishStep);
                new Application().Run(view);
            }
        }

        private static IWizardStep<IStepView> GetFinishStep(WizardModel model)
        {
            return new FinishStep(model, new WpfFinishStepView());
        }

        private static void PrintPlatformInfo()
        {
            Console.WriteLine(GetPlatformInfo());
        }

        private static string GetPlatformInfo()
        {
            return new StringBuilder()
                .Append("OS version: ").AppendLine(Environment.OSVersion.ToString())
                .Append("Runtime: ").AppendLine(Process.GetCurrentProcess().ProcessName)
                .ToString();
        }

        private static IEnumerable<IWizardStep<IStepView>> GetSteps(WizardModel model)
        {
            var fileSystemManager = new FileSystemManager();
            var folderAutoCompleteProvider = new FileSystemAutoCompleteProvider(fileSystemManager, CompletionType.FoldersOnly);
            var steps = new List<IWizardStep<IStepView>>();
            steps.Add(new SelectProfileStep(model, new WpfSelectProfileStepView(), ProfileManager.Instance));
            steps.Add(new SetUpBackupStep(model, new WpfSetUpBackupStepView(folderAutoCompleteProvider), fileSystemManager));
            steps.Add(new SetUpDeployStatusStep(model, new WpfSetUpDeployStatusStepView(), fileSystemManager));
            steps.Add(new SetUpGenerateWebConfigStep(model, new WpfSetUpGenerateWebConfigStepView(), fileSystemManager));
            steps.Add(new SetUpDestinationStep(model, new WpfSetUpDestinationStepView(), fileSystemManager));
            steps.Add(new SelectPackageStep(model, new WpfSelectPackageStepView(), fileSystemManager));
            steps.Add(new SummaryStep(model, new WpfSummaryStepView()));
            return steps;
        }

        private static bool RunningWindows()
        {
            return true;
        }
    }
}
