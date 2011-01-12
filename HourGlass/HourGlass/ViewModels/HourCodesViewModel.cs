using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using HourGlass.Commands;
using HourGlass.Providers;

namespace HourGlass.ViewModels
{
    public class HourCodesViewModel : ViewModelBase, IHourCodesViewModel
    {
        public ICommand SaveCurrentCodeCommand { get; private set; }
        public ICommand RemoveCurrentCodeCommand { get; private set; }

        public ObservableCollection<HourCodeViewModel> HourCodes { get { return _hourCodeProvider.AvailableHourCodes; } }
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

        private readonly IHourCodeProvider _hourCodeProvider;

        public HourCodesViewModel(IHourCodeProvider hourCodeProvider)
        {
            _hourCodeProvider = hourCodeProvider;
            SaveCurrentCodeCommand = new DelegateCommand(SaveCurrentCode);
            RemoveCurrentCodeCommand = new DelegateCommand(RemoveCurrentCode);
        }

        private void SaveCurrentCode(object obj)
        {
            _hourCodeProvider.Save(CurrentCode);
        }

        private void RemoveCurrentCode(object obj)
        {
            if (CurrentCode == null)
            {
                return;
            }

            var currentCode = CurrentCode;
            _hourCodeProvider.Remove(currentCode);
            CurrentCode = HourCodes.FirstOrDefault();
        }
    }
}