using System;
using System.Windows;
using DeployWizard.Lib.Steps;
using DeployWizard.Lib.Steps.Views;
using DeployWizard.Lib.Views;

namespace DeployWizard.Wpf.Views
{
    public partial class WpfDeployWizardView : IDeployWizardView
    {
        public event EventHandler PreviousClicked;
        public event EventHandler NextClicked;
        public event EventHandler FinishClicked;
        public event EventHandler CloseClicked;
        public event EventHandler SaveClicked;
        public event EventHandler FastForwardClicked;

        public void ShowStep(IWizardStep<IStepView> step)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add((UIElement) step.View);
        }

        public void ShowError(Exception exception)
        {
            Status.Text = exception == null ? string.Empty : exception.Message;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void PrepareToClose()
        {
            FinishButton.Content = "Close";
        }

        public WpfDeployWizardView()
        {
            InitializeComponent();
        }

        #region Events
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            PreviousClicked(sender, e);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NextClicked(sender, e);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveClicked(sender, e);
        }

        private void FastForwardButton_Click(object sender, RoutedEventArgs e)
        {
            FastForwardClicked(sender, e);
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            if (!FinishButton.Content.Equals("Close"))
            {
                FinishClicked(sender, e);
            }
            else
            {
                CloseClicked(sender, e);
            }
        }
        #endregion

        #region Enable buttons
        public void SetPreviousEnabled(bool enabled)
        {
            PreviousButton.IsEnabled = enabled;
        }

        public void SetNextEnabled(bool enabled)
        {
            NextButton.IsEnabled = enabled;
        }

        public void SetFastForwardEnabled(bool enabled)
        {
            FastForwardButton.IsEnabled = enabled;
        }

        public void SetFinishEnabled(bool enabled)
        {
            FinishButton.IsEnabled = enabled;
        }
        #endregion
    }
}
