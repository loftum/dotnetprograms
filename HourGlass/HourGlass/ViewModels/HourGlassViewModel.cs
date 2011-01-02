namespace HourGlass.ViewModels
{
    public class HourGlassViewModel : IHourGlassViewModel
    {
        public IWeeksViewModel WeeksViewModel { get; private set; }
        public IHourCodesViewModel HourCodesViewModel { get; private set; }

        public HourGlassViewModel(IWeeksViewModel weeksViewModel, IHourCodesViewModel hourCodesViewModel)
        {
            WeeksViewModel = weeksViewModel;
            HourCodesViewModel = hourCodesViewModel;
        }
    }
}