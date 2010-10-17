namespace DeployWizard.Lib.Steps.Views
{
    public interface IFinishStepView : IStepView
    {
        void ReportProgress(int current, int total);
        void AppendMessage(string message);
    }
}