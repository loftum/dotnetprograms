using System.Collections.ObjectModel;
using HourGlass.ViewModels;

namespace HourGlass.Providers
{
    public interface IHourCodeProvider
    {
        ObservableCollection<HourCodeViewModel> AvailableHourCodes { get; }
        HourCodeViewModel Add(string code, string name);
        void Remove(HourCodeViewModel model);
        void Save(HourCodeViewModel model);
    }
}