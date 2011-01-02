using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using HourGlass.Commands;
using HourGlass.Lib.Services;

namespace HourGlass.ViewModels
{
    public class HourCodesViewModel : ViewModelBase, IHourCodesViewModel
    {
        public ICommand SaveCurrentCodeCommand { get; private set; }
        public ICommand RemoveCurrentCodeCommand { get; private set; }

        public ObservableCollection<HourCodeViewModel> HourCodes { get; private set; }
        private HourCodeViewModel _currentCode;
        public HourCodeViewModel CurrentCode
        {
            get
            {
                return _currentCode;
            }
            set
            {
                _currentCode = value;
                OnPropertyChanged("CurrentCode");
            }
        }

        private readonly IHourCodeService _hourCodeService;

        public HourCodesViewModel(IHourCodeService hourCodeService)
        {
            _hourCodeService = hourCodeService;
            HourCodes = new ObservableCollection<HourCodeViewModel>();
            RefreshHourCodes();
            _hourCodeService.HourCodes.CollectionChanged += HandleHourCodesChanged;
            SaveCurrentCodeCommand = new DelegateCommand(SaveCurrentCode);
            RemoveCurrentCodeCommand = new DelegateCommand(RemoveCurrentCode);
        }

        private void HandleHourCodesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshHourCodes();
        }

        private void RefreshHourCodes()
        {
            HourCodes.Clear();
            foreach (var hourCode in _hourCodeService.HourCodes)
            {
                HourCodes.Add(new HourCodeViewModel(_hourCodeService, hourCode));
            }
        }

        private void SaveCurrentCode(object obj)
        {
            _hourCodeService.Save(CurrentCode.HourCode);
        }

        private void RemoveCurrentCode(object obj)
        {
            if (CurrentCode == null)
            {
                return;
            }

            var currentCode = CurrentCode;
            HourCodes.Remove(currentCode);
            CurrentCode = HourCodes.FirstOrDefault();
            _hourCodeService.Remove(currentCode.HourCode);
        }
    }
}