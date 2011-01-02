using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HourGlass.ViewModels
{
    public interface IHourCodesViewModel
    {
        ICommand SaveCurrentCodeCommand { get; }
        ICommand RemoveCurrentCodeCommand { get; }
        ObservableCollection<HourCodeViewModel> HourCodes { get; }
        HourCodeViewModel CurrentCode { get; set; }
    }
}