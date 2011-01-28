using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EnvironmentViewer.Commands;
using EnvironmentViewer.Lib.Data;

namespace EnvironmentViewer.ViewModels
{
    public class EnvironmentsViewModel : ViewModelBase
    {
        public ICommand AddEnvironmentCommand { get; private set; }
        public ICommand SaveAllEnvironmentsCommand { get; private set; }

        public ObservableCollection<string> AvailableDatabaseTypes { get; private set; }
        public ObservableCollection<EnvironmentViewModel> Environments { get; private set; }

        private EnvironmentViewModel _currentEnvironment;
        public EnvironmentViewModel CurrentEnvironment
        {
            get { return _currentEnvironment; }
            set { _currentEnvironment = value; OnPropertyChanged("CurrentEnvironment"); }
        }

        private readonly IEnvironmentRepo _repo;

        public EnvironmentsViewModel()
        {
            Environments = new ObservableCollection<EnvironmentViewModel>();
            AvailableDatabaseTypes = new ObservableCollection<string>(new []{"sqlserver", "mysql", "sqlite"});
            _repo = new XmlEnvironmentRepo("environments.xml");
            LoadEnvironments();
            AddEnvironmentCommand = new DelegateCommand(AddEnvironment);
            SaveAllEnvironmentsCommand = new DelegateCommand(SaveAllEnvironments);
        }

        private void SaveAllEnvironments(object obj)
        {
            var environments = Environments.Select(env => env.Environment);
            _repo.SaveAll(environments);
        }

        private void LoadEnvironments()
        {
            var environments = _repo.GetAll();
            foreach (var environment in environments)
            {
                Environments.Add(new EnvironmentViewModel(this, environment));
            }
        }

        public void AddEnvironment(object obj)
        {
            Environments.Add(new EnvironmentViewModel(this));
        }

        public void RemoveEnvironment(EnvironmentViewModel environmentViewModel)
        {
            Environments.Remove(environmentViewModel);
        }
    }
}