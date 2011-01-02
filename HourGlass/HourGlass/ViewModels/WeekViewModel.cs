using System.Collections.ObjectModel;
using System.Windows.Input;
using HourGlass.Commands;
using HourGlass.Lib.Data;
using HourGlass.Lib.Domain;

namespace HourGlass.ViewModels
{
    public class WeekViewModel : ViewModelBase
    {
        public ICommand SaveWeekCommand { get; private set; }
        public ICommand AddUsageCommand { get; private set; }
        public ObservableCollection<UsageViewModel> Usages { get; set; }

        private readonly Week _week;
        private readonly IHourGlassRepo _repo;


        public ObservableCollection<HourCode> AvailableHourCodes { get; private set; }


        public WeekViewModel(IHourGlassRepo repo, Week week)
        {
            _repo = repo;
            _week = week;
            AvailableHourCodes = new ObservableCollection<HourCode>();
            foreach (var hourCode in _repo.GetAll<HourCode>())
            {
                AvailableHourCodes.Add(hourCode);
            }

            Usages = new ObservableCollection<UsageViewModel>();
            foreach (var hourUsage in _week.Usages)
            {
                Usages.Add(new UsageViewModel(this, hourUsage));
            }
            SaveWeekCommand = new DelegateCommand(SaveWeek);
            AddUsageCommand = new DelegateCommand(AddUsage);
        }

        public void AddHourCode(object parameter)
        {
            var codeAndName = parameter.ToString();
            var split = codeAndName.Split(new[] {':'});
            var hourCode = new HourCode
            {
                Code = split[0].Trim(),
                Name = split.Length > 1 ? split[1].Trim() : string.Empty
            };
            _repo.Save(hourCode);
        }

        private void AddUsage(object parameter)
        {
            var usage = new HourUsage();
            _week.AddUsage(usage);
            Usages.Add(new UsageViewModel(this, usage));
        }

        private void SaveWeek(object parameter)
        {
            _repo.Save(_week);
        }

        public int Year
        {
            get { return _week.Year; }
        }

        public int Number
        {
            get { return _week.Number; }
        }

        public double Monday
        {
            get { return _week.Monday; }
        }

        public double Tuesday
        {
            get { return _week.Tuesday; }
        }

        public double Wednesday
        {
            get { return _week.Wednesday; }
        }

        public double Thursday
        {
            get { return _week.Thursday; }
        }

        public double Friday
        {
            get { return _week.Friday; }
        }

        public double Saturday
        {
            get { return _week.Saturday; }
        }

        public double Sunday
        {
            get { return _week.Sunday; }
        }

        public double Sum
        {
            get { return _week.Sum; }
        }

        public override string ToString()
        {
            return _week.ToString();
        }
    }
}