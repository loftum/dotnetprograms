using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using HourGlass.Commands;
using HourGlass.Lib.Domain;
using HourGlass.Lib.Services;

namespace HourGlass.ViewModels
{
    public class HourUsageViewModel : ViewModelBase
    {
        public ICommand RemoveCommand { get; private set; }
        public ICommand AddHourCodeCommand { get; private set; }

        public HourUsage Usage { get; private set; }

        private readonly IHourCodeService _hourCodeService;
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

        public ObservableCollection<HourCodeViewModel> AvailableHourCodes { get; private set;}

        public HourUsageViewModel(IHourCodeService hourCodeService, WeekViewModel weekViewModel, HourUsage usage)
        {
            _hourCodeService = hourCodeService;
            Usage = usage;
            _weekViewModel = weekViewModel;
            AvailableHourCodes = new ObservableCollection<HourCodeViewModel>();
            RefreshHourCodes();
            AvailableHourCodes.CollectionChanged += HandleHourCodesChanged;
            RemoveCommand = new DelegateCommand(Remove);
            AddHourCodeCommand = new DelegateCommand(AddHourCode);
            HourCode = new HourCodeViewModel(_hourCodeService, usage.HourCode);
        }

        private void HandleHourCodesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshHourCodes();
        }

        private void RefreshHourCodes()
        {
            AvailableHourCodes.Clear();
            foreach (var hourCode in _hourCodeService.HourCodes)
            {
                AvailableHourCodes.Add(new HourCodeViewModel(_hourCodeService, hourCode));
            }
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
            var hourCode = _hourCodeService.AddHourCode(code, name);
            HourCode = new HourCodeViewModel(_hourCodeService, hourCode);
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