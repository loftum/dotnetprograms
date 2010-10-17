using System;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfFinishStepView : IFinishStepView
    {
        public WpfFinishStepView()
        {
            InitializeComponent();
        }

        public void ValidateAll()
        {
            
        }

        public void ReportProgress(int current, int total)
        {
            ProgressBar.Value = (double) current/total;
        }

        public void AppendMessage(string message)
        {
            LogBlock.Text += message + Environment.NewLine;
        }
    }
}
