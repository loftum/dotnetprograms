using System.Collections.ObjectModel;
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
            AvailableHourCodes = new ObservableCollection<HourCodeViewModel>();
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
            var model = new HourCodeViewModel(this, hourCode);
            AvailableHourCodes.Add(model);
            return model;
        }

        public void Remove(HourCodeViewModel model)
        {
            AvailableHourCodes.Remove(model);
            _hourCodeService.Remove(model.HourCode);
        }

        public void Save(HourCodeViewModel model)
        {
            _hourCodeService.Save(model.HourCode);
        }
    }
}