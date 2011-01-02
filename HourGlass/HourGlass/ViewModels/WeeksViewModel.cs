using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HourGlass.Commands;
using HourGlass.Lib.Services;

namespace HourGlass.ViewModels
{
    public class WeeksViewModel : ViewModelBase, IWeeksViewModel
    {
        public ICommand AddWeekCommand { get; private set; }
        public ICommand RemoveWeekCommand { get; private set; }
        public ObservableCollection<WeekViewModel> Weeks { get; private set; }
        private WeekViewModel _currentWeek;
        public WeekViewModel CurrentWeek
        {
            get { return _currentWeek; }
            set
            { 
                _currentWeek = value;
                OnPropertyChanged("CurrentWeek");
            }
        }

        private readonly IHourCodeService _hourCodeService;
        private readonly IWeekService _weekService;

        public WeeksViewModel(IWeekService weekService, IHourCodeService hourCodeService)
        {
            _weekService = weekService;
            _hourCodeService = hourCodeService;
            AddWeekCommand = new DelegateCommand(AddWeek);
            RemoveWeekCommand = new DelegateCommand(RemoveWeek);
            Weeks = new ObservableCollection<WeekViewModel>();
            foreach(var week in _weekService.GetRecentWeeks())
            {
                Weeks.Add(new WeekViewModel(_weekService, _hourCodeService, week));
            }
            CurrentWeek = Weeks.FirstOrDefault();
        }

        private void RemoveWeek(object obj)
        {
            if (CurrentWeek == null)
            {
                return;
            }
            var currentWeek = CurrentWeek;
            Weeks.Remove(currentWeek);
            CurrentWeek = Weeks.FirstOrDefault();
            _weekService.Remove(currentWeek.Week);
        }

        private void AddWeek(object parameter)
        {
            var week = _weekService.NewWeek(GetMaxStartDate());
            var viewModel = new WeekViewModel(_weekService, _hourCodeService, week);
            Weeks.Add(viewModel);
            CurrentWeek = viewModel;
        }

        private DateTime GetMaxStartDate()
        {
            return Weeks.Count > 0 ?
                Weeks.Max(model => model.Week.StartDate) :
                new DateTime();
        }
    }
}