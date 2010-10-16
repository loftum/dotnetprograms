using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSummaryStepView : ISummaryStepView
    {
        public string Summary
        {
            get { return SummaryBlock.Text; }
            set { SummaryBlock.Text = value; }
        }

        public WpfSummaryStepView()
        {
            InitializeComponent();
        }

        public void ValidateAll()
        {
            
        }
    }
}
