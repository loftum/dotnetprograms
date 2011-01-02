using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HourGlass.Commands;
using HourGlass.Lib.Domain;
using HourGlass.Lib.Services;

namespace HourGlass.ViewModels
{
    public class UsageViewModel : ViewModelBase
    {
        public ICommand RemoveCommand { get; private set; }
        public ICommand AddHourCodeCommand { get; private set; }

        private readonly IHourCodeService _hourCodeService;
        private readonly HourUsage _usage;
        private readonly WeekViewModel _weekViewModel;

        public ObservableCollection<HourCode> AvailableHourCodes
        {
            get { return _hourCodeService.HourCodes; }
        }

        public UsageViewModel(IHourCodeService hourCodeService, WeekViewModel weekViewModel, HourUsage usage)
        {
            _hourCodeService = hourCodeService;
            _usage = usage;
            _weekViewModel = weekViewModel;
            RemoveCommand = new DelegateCommand(Remove);
            AddHourCodeCommand = new DelegateCommand(AddHourCode);
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
            _usage.HourCode = hourCode;

        }

        private void Remove(object parameter)
        {
            _weekViewModel.Usages.Remove(this);
        }

        public HourCode HourCode
        {
            get { return _usage.HourCode; }
            set 
            {
                _usage.HourCode = value;
                OnPropertyChanged("HourCode");
            }
        }

        public double Monday
        {
            get { return _usage.Monday; }
            set
            {
                _usage.Monday = value;
                OnPropertyChanged("Monday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Tuesday
        {
            get { return _usage.Tuesday; }
            set
            {
                _usage.Tuesday = value;
                OnPropertyChanged("Tuesday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Wednesday
        {
            get { return _usage.Wednesday; }
            set
            {
                _usage.Wednesday = value;
                OnPropertyChanged("Wednesday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Thursday
        {
            get { return _usage.Thursday; }
            set
            {
                _usage.Thursday = value;
                OnPropertyChanged("Thursday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Friday
        {
            get { return _usage.Friday; }
            set
            {
                _usage.Friday = value;
                OnPropertyChanged("Friday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Saturday
        {
            get { return _usage.Saturday; }
            set
            {
                _usage.Saturday = value;
                OnPropertyChanged("Saturday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Sunday
        {
            get { return _usage.Sunday; }
            set
            {
                _usage.Sunday = value;
                OnPropertyChanged("Sunday");
                OnPropertyChanged("Sum");
                _weekViewModel.NumbersChanged();
            }
        }

        public double Sum
        {
            get { return _usage.Sum; }
        }
    }
}