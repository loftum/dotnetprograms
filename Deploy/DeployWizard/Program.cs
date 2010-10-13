using System;
using System.Collections.Generic;
using System.Windows;
using DeployWizard.Lib.Controllers;
using DeployWizard.Lib.Steps;
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
                var steps = new List<IWizardStep>();
                new DeployWizardController(view, steps);
                new Application().Run(view);
            }
        }

        private static bool RunningWindows()
        {
            return true;
        }
    }
}
