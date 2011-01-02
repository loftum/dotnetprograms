using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HourGlass.Commands;
using HourGlass.Lib.Domain;
using HourGlass.Lib.Services;

namespace HourGlass.ViewModels
{
    public class WeekViewModel : ViewModelBase
    {
        public ICommand SaveWeekCommand { get; private set; }
        public ICommand AddUsageCommand { get; private set; }
        public ObservableCollection<UsageViewModel> Usages { get; set; }

        public Week Week{ get; private set;}
        private readonly IWeekService _weekService;
        private readonly IHourCodeService _hourCodeService;

        public WeekViewModel(IWeekService weekService, IHourCodeService hourCodeService, Week week)
        {
            _weekService = weekService;
            Week = week;
            _hourCodeService = hourCodeService;

            Usages = new ObservableCollection<UsageViewModel>();
            foreach (var hourUsage in Week.Usages)
            {
                Usages.Add(new UsageViewModel(_hourCodeService, this, hourUsage));
            }
            SaveWeekCommand = new DelegateCommand(SaveWeek);
            AddUsageCommand = new DelegateCommand(AddUsage);
        }

        private void AddUsage(object parameter)
        {
            var usage = new HourUsage();
            Week.AddUsage(usage);
            Usages.Add(new UsageViewModel(_hourCodeService, this, usage));
        }

        private void SaveWeek(object parameter)
        {
            _weekService.Save(Week);
        }
        
        public void NumbersChanged()
        {
            OnPropertiesChanged("Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Sum");
        }

        public DateTime StartDate
        {
            get { return Week.StartDate; }
            set
            {
                var day = value;
                while (day.DayOfWeek != DayOfWeek.Monday)
                {
                    day = day.AddDays(-1);
                }
                Week.StartDate = day;
                OnPropertyChanged("StartDate");
                OnPropertyChanged("Year");
                OnPropertyChanged("Number");
            }
        }

        public int Year
        {
            get { return Week.Year; }
        }

        public int Number
        {
            get { return Week.Number; }
        }

        public double Monday
        {
            get { return Week.Monday; }
        }

        public double Tuesday
        {
            get { return Week.Tuesday; }
        }

        public double Wednesday
        {
            get { return Week.Wednesday; }
        }

        public double Thursday
        {
            get { return Week.Thursday; }
        }

        public double Friday
        {
            get { return Week.Friday; }
        }

        public double Saturday
        {
            get { return Week.Saturday; }
        }

        public double Sunday
        {
            get { return Week.Sunday; }
        }

        public double Sum
        {
            get { return Week.Sum; }
        }

        public override string ToString()
        {
            return Week.ToString();
        }
    }
}