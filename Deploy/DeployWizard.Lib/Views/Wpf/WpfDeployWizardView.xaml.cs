using System;
using System.Windows;

namespace DeployWizard.Lib.Views.Wpf
{
    public partial class WpfDeployWizardView : IDeployWizardView
    {
        public event EventHandler PreviousClicked;
        public event EventHandler NextClicked;
        public event EventHandler FinishClicked;
        
        public void SetPreviousEnabled(bool enabled)
        {
            PreviousButton.IsEnabled = enabled;
        }

        public void SetNextEnabled(bool enabled)
        {
            NextButton.IsEnabled = enabled;
        }

        public void SetFinishEnabled(bool enabled)
        {
            FinishButton.IsEnabled = enabled;
        }

        public WpfDeployWizardView()
        {
            InitializeComponent();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            PreviousClicked(sender, e);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NextClicked(sender, e);
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            FinishClicked(sender, e);
        }
    }
}
