using System.Collections.ObjectModel;
using System.Windows.Input;
using HourGlass.Commands;
using HourGlass.Lib.Data;
using HourGlass.Lib.DateAndTime;
using HourGlass.Lib.Domain;

namespace HourGlass.ViewModels
{
    public class HourGlassViewModel : ViewModelBase, IHourGlassViewModel
    {
        public ICommand AddWeekCommand { get; private set; }

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

        private readonly IHourGlassRepo _repo;
        private readonly IDateProvider _dateProvider;

        public HourGlassViewModel(IHourGlassRepo repo, IDateProvider dateProvider)
        {
            _repo = repo;
            _dateProvider = dateProvider;
            AddWeekCommand = new DelegateCommand(AddWeek);
            Weeks = new ObservableCollection<WeekViewModel>();
            foreach(var week in _repo.GetAll<Week>())
            {
                Weeks.Add(new WeekViewModel(_repo, week));
            }
        }

        private void AddWeek(object parameter)
        {
            var week = new Week
                {
                    Year = _dateProvider.GetCurrentYear(),
                    StartDate = _dateProvider.GetCurrentWeekStartDate()
                };
            
            var viewModel = new WeekViewModel(_repo, week);
            Weeks.Add(viewModel);
            CurrentWeek = viewModel;
        }
    }
}