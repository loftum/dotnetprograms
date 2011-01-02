using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HourGlass.ViewModels
{
    public interface IHourGlassViewModel
    {
        ICommand AddWeekCommand { get; }
        ObservableCollection<WeekViewModel> Weeks { get; }
        WeekViewModel CurrentWeek { get; set; }
    }
}