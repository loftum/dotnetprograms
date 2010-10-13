using System;
using System.Windows;
using DeployWizard.Components;
using DeployWizard.StepProcess;

namespace DeployWizard
{
    public partial class MainWindow : IWizardView
    {
        private readonly StepController _controller;

        public MainWindow()
        {
            InitializeComponent();
            _controller = new StepController(this);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _controller.Next();
        }

        public void ShowStep(ISetupStep setupStep)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add((UIElement)setupStep);
        }

        public void SetNextEnabled(bool enabled)
        {
            NextButton.IsEnabled = enabled;
        }

        public void SetFinishEnabled(bool enabled)
        {
            FinishButton.IsEnabled = enabled;
        }

        public void SetPreviousEnabled(bool enabled)
        {
            PreviousButton.IsEnabled = enabled;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            _controller.Previous();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            _controller.Finish();
        }
    }
}
