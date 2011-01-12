using System.Collections.ObjectModel;
using System.Collections.Specialized;
using HourGlass.Lib.Services;
using HourGlass.ViewModels;

namespace HourGlass.Providers
{
    public class HourCodeProvider : IHourCodeProvider
    {
        private readonly IHourCodeService _hourCodeService;

        public ObservableCollection<HourCodeViewModel> AvailableHourCodes { get; private set; }

        public HourCodeProvider(IHourCodeService hourCodeService)
        {
            _hourCodeService = hourCodeService;
            _hourCodeService.HourCodes.CollectionChanged += HandleCollectionChanged;
            AvailableHourCodes = new ObservableCollection<HourCodeViewModel>();
            RefreshHourCodes();
        }

        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshHourCodes();
        }

        private void RefreshHourCodes()
        {
            AvailableHourCodes.Clear();
            foreach (var hourCode in _hourCodeService.HourCodes)
            {
                AvailableHourCodes.Add(new HourCodeViewModel(this, hourCode));
            }
        }

        public HourCodeViewModel Add(string code, string name)
        {
            var hourCode = _hourCodeService.AddHourCode(code, name);
            return new HourCodeViewModel(this, hourCode);
        }

        public void Remove(HourCodeViewModel model)
        {
            _hourCodeService.Remove(model.HourCode);
        }

        public void Save(HourCodeViewModel model)
        {
            _hourCodeService.Save(model.HourCode);
        }
    }
}