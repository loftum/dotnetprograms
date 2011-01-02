namespace HourGlass.ViewModels
{
    public interface IHourGlassViewModel
    {
        IWeeksViewModel WeeksViewModel { get; }
        IHourCodesViewModel HourCodesViewModel { get; }
    }
}