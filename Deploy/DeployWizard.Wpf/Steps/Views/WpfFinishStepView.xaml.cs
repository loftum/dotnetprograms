using System;
using System.Windows.Threading;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfFinishStepView : IFinishStepView
    {
        public delegate void DoSomething();

        public WpfFinishStepView()
        {
            InitializeComponent();
        }

        public void ValidateAll()
        {
            
        }

        public void ReportProgress(int current, int total)
        {
            ProgressBar.Dispatcher.BeginInvoke(
                    new DoSomething(() => ProgressBar.Value = (double) 100 * current / total),
                    DispatcherPriority.Normal
                );
        }

        public void AppendMessage(string message)
        {
            LogBlock.Dispatcher.BeginInvoke(
                new DoSomething(() => LogBlock.Text += message + Environment.NewLine),
                DispatcherPriority.Normal
                );
        }
    }
}
