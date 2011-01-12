using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using HourGlass.Commands;
using HourGlass.Lib.Domain;
using HourGlass.Lib.Services;
using HourGlass.Providers;

namespace HourGlass.ViewModels
{
    public class HourUsageViewModel : ViewModelBase
    {
        public ICommand RemoveCommand { get; private set; }
        public ICommand AddHourCodeCommand { get; private set; }

        public HourUsage Usage { get; private set; }

        private readonly IHourCodeProvider _hourCodeProvider;
        private readonly WeekViewModel _weekViewModel;
        
        private HourCodeViewModel _hourCode;
        public HourCodeViewModel HourCode
        {
            get { return _hourCode; }
            set
            {
                _hourCode = value;
                Usage.SetHourCode(_hourCode == null ? null : _hourCode.HourCode);
                OnPropertyChanged("HourCode");
            }
        }

        public ObservableCollection<HourCodeViewModel> AvailableHourCodes { get { return _hourCodeProvider.AvailableHourCodes; } }

        public HourUsageViewModel(IHourCodeProvider hourCodeProvider, WeekViewModel weekViewModel, HourUsage usage)
        {
            _hourCodeProvider = hourCodeProvider;
            Usage = usage;
            _weekViewModel = weekViewModel;
            RemoveCommand = new DelegateCommand(Remove);
            AddHourCodeCommand = new DelegateCommand(AddHourCode);
            HourCode = new HourCodeViewModel(_hourCodeProvider, usage.HourCode);
        }

        private void AddHourCode(object parameter)
        {
            var codeAndName = parameter.ToString();
            var split = codeAndName.Split(new[] { ':' });
            if (split.Length < 2)
            {
                return;
            }
            var code = split[0];
            var name = split[1];
            HourCode = _hourCodeProvider.Add(code, name);
        }

        private void Remove(object parameter)
        {
            Usage.SetHourCode(null);
            _weekViewModel.Remove(this);
        }

        public double Monday
        {
            get { return Usage.Monday; }
            set
            {
                Usage.Monday = value;
                OnPropertyChanged("Monday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Tuesday
        {
            get { return Usage.Tuesday; }
            set
            {
                Usage.Tuesday = value;
                OnPropertyChanged("Tuesday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Wednesday
        {
            get { return Usage.Wednesday; }
            set
            {
                Usage.Wednesday = value;
                OnPropertyChanged("Wednesday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Thursday
        {
            get { return Usage.Thursday; }
            set
            {
                Usage.Thursday = value;
                OnPropertyChanged("Thursday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Friday
        {
            get { return Usage.Friday; }
            set
            {
                Usage.Friday = value;
                OnPropertyChanged("Friday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Saturday
        {
            get { return Usage.Saturday; }
            set
            {
                Usage.Saturday = value;
                OnPropertyChanged("Saturday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Sunday
        {
            get { return Usage.Sunday; }
            set
            {
                Usage.Sunday = value;
                OnPropertyChanged("Sunday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Sum
        {
            get { return Usage.Sum; }
        }
    }
}