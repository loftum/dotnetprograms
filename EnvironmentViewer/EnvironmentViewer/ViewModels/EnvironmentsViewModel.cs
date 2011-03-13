using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EnvironmentViewer.Commands;
using EnvironmentViewer.Lib.Data;
using EnvironmentViewer.Lib.Services;

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

        private readonly IEnvironmentService _environmentService;
        private readonly IEnvironmentViewerRepo _repo;

        public EnvironmentsViewModel(IEnvironmentService environmentService)
        {
            _environmentService = environmentService;
            Environments = new ObservableCollection<EnvironmentViewModel>();
            AvailableDatabaseTypes = new ObservableCollection<string>(new []{"none", "sqlserver", "mysql", "sqlite"});
            _repo = new XmlEnvironmentViewerRepo("environments.xml");
            LoadEnvironments();
            AddEnvironmentCommand = new DelegateCommand(AddEnvironment);
            SaveAllEnvironmentsCommand = new DelegateCommand(SaveAllEnvironments);
        }

        private void SaveAllEnvironments(object obj)
        {
            var environments = Environments.Select(env => env.EnvironmentData);
            _repo.SaveAll(environments);
        }

        private void LoadEnvironments()
        {
            var environments = _repo.GetAll();
            foreach (var environment in environments)
            {
                Environments.Add(new EnvironmentViewModel(this, _environmentService, environment));
            }
        }

        public void AddEnvironment(object obj)
        {
            Environments.Add(new EnvironmentViewModel(this, _environmentService));
        }

        public void RemoveEnvironment(EnvironmentViewModel environmentViewModel)
        {
            Environments.Remove(environmentViewModel);
        }
    }
}