using System.Collections.ObjectModel;
using System.Windows.Input;
using EnvironmentViewer.Commands;
using EnvironmentViewer.Lib.Domain;
using EnvironmentViewer.Lib.Extensions;
using EnvironmentViewer.Lib.Services;

namespace EnvironmentViewer.ViewModels
{
    public class EnvironmentViewModel : ViewModelBase
    {
        private readonly IEnvironmentService _environmentService;
        private EnvironmentState _state = new EnvironmentState();

        public ICommand AddEnvironmentCommand { get; private set; }
        public ICommand RemoveEnvironmentCommand { get; private set; }
        public ICommand UpdateStatusCommand { get; private set; }

        public ObservableCollection<string> AvailableDatabaseTypes { get; private set; }
        private readonly EnvironmentsViewModel _environmentsViewModel;
        public EnvironmentData EnvironmentData { get; private set; }

        public EnvironmentViewModel(EnvironmentsViewModel environmentsViewModel, IEnvironmentService environmentService)
            : this (environmentsViewModel, environmentService, new EnvironmentData{ Name="Name", Host = "Host" })
        {
        }

        public EnvironmentViewModel(EnvironmentsViewModel environmentsViewModel,
            IEnvironmentService environmentService,
            EnvironmentData environmentData)
        {
            _environmentsViewModel = environmentsViewModel;
            _environmentService = environmentService;
            EnvironmentData = environmentData;
            AddEnvironmentCommand = new DelegateCommand(AddEnvironment);
            RemoveEnvironmentCommand = new DelegateCommand(RemoveEnvironment);
            UpdateStatusCommand = new DelegateCommand(UpdateStatus);
            AvailableDatabaseTypes = _environmentsViewModel.AvailableDatabaseTypes;
            UpdateStatus(null);
        }

        private void UpdateStatus(object obj)
        {
            _state = _environmentService.GetStateOf(EnvironmentData);
            OnPropertiesChanged("ApplicationVersion", "ApplicationStatus", "DatabaseVersion", "DatabaseStatus");
        }

        private void AddEnvironment(object obj)
        {
            _environmentsViewModel.AddEnvironment(obj);
        }

        private void RemoveEnvironment(object obj)
        {
            _environmentsViewModel.RemoveEnvironment(this);
        }

        public string Name
        {
            get { return EnvironmentData.Name; }
            set { EnvironmentData.Name = value; OnPropertyChanged("Name"); }
        }

        public string Host
        {
            get { return EnvironmentData.Host; }
            set { EnvironmentData.Host = value; OnPropertyChanged("Host"); }
        }

        public string Url
        {
            get { return EnvironmentData.Url; }
            set { EnvironmentData.Url = value; OnPropertyChanged("Url"); }
        }

        public string DatabaseType
        {
            get { return EnvironmentData.DatabaseType; }
            set { EnvironmentData.DatabaseType = value; OnPropertiesChanged("DatabaseType", "IntegratedSecurityEnabled"); }
        }

        public string DatabaseHost
        {
            get { return EnvironmentData.DatabaseHost; }
            set { EnvironmentData.DatabaseHost = value; OnPropertyChanged("DatabaseHost"); }
        }

        public string DatabaseName
        {
            get { return EnvironmentData.DatabaseName; }
            set { EnvironmentData.DatabaseName = value; OnPropertyChanged("DatabaseName"); }
        }

        public string DatabaseUsername
        {
            get { return EnvironmentData.DatabaseUsername; }
            set { EnvironmentData.DatabaseUsername = value; OnPropertyChanged("DatabaseUsername"); }
        }

        public string DatabasePassword
        {
            get { return "*********"; }
            set { EnvironmentData.DatabasePassword = value; OnPropertyChanged("DatabasePassword"); }
        }

        public bool IntegratedSecurity
        {
            get { return EnvironmentData.IntegratedSecurity; }
            set { EnvironmentData.IntegratedSecurity = value; OnPropertiesChanged("IntegratedSecurity"); }
        }

        public bool IntegratedSecurityEnabled
        {
            get { return !DatabaseType.IsNullOrEmpty() && DatabaseType.Equals("sqlserver"); }
        }

        public string ApplicationVersion
        {
            get { return _state.ApplicationVersion; }
        }

        public string ApplicationStatus
        {
            get { return _state.ApplicationStatus; }
        }

        public string DatabaseVersion
        {
            get { return _state.DatabaseVersion; }
        }

        public string DatabaseStatus
        {
            get { return _state.DatabaseStatus; }
        }

        public override string ToString()
        {
            return Name.IsNullOrEmpty() ? "Unknown" : Name;
        }
    }
}