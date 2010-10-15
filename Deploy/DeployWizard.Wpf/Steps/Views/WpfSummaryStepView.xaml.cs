using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSummaryStepView : ISummaryStepView
    {
        public string Summary
        {
            get; set;
        }

        public WpfSummaryStepView()
        {
            InitializeComponent();
            Binder.Bind(this, "Summary")
                .WithTargetNullValue(string.Empty)
                .ToTextBlock(SummaryBlock);
        }

        public void ValidateAll()
        {
            
        }
    }
}
