namespace DeployWizard.Lib.SummaryFormatting
{
    public interface ISummaryFormatter<in T>
    {
        string Format(T model);
    }
}