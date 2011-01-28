using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using EnvironmentViewer.Commands;
using EnvironmentViewer.Lib.Extensions;
using Environment = EnvironmentViewer.Lib.Domain.Environment;

namespace EnvironmentViewer.ViewModels
{
    public class EnvironmentViewModel : ViewModelBase
    {
        public ICommand AddEnvironmentCommand { get; private set; }
        public ICommand RemoveEnvironmentCommand { get; private set; }

        public ObservableCollection<string> AvailableDatabaseTypes { get; private set; }
        private readonly EnvironmentsViewModel _environmentsViewModel;
        public Environment Environment { get; private set; }

        public EnvironmentViewModel(EnvironmentsViewModel environmentsViewModel)
            : this (environmentsViewModel, new Environment{Name="Name", Host = "Host"})
        {
        }

        public EnvironmentViewModel(EnvironmentsViewModel environmentsViewModel, Environment environment)
        {
            Environment = environment;
            AddEnvironmentCommand = new DelegateCommand(AddEnvironment);
            RemoveEnvironmentCommand = new DelegateCommand(RemoveEnvironment);
            _environmentsViewModel = environmentsViewModel;
            AvailableDatabaseTypes = _environmentsViewModel.AvailableDatabaseTypes;
        }

        private void AddEnvironment(object obj)
        {
            MessageBox.Show("Add environment");
            _environmentsViewModel.AddEnvironment(obj);
        }

        private void RemoveEnvironment(object obj)
        {
            _environmentsViewModel.RemoveEnvironment(this);
        }

        public string Name
        {
            get { return Environment.Name; }
            set { Environment.Name = value; OnPropertyChanged("Name"); }
        }

        public string Host
        {
            get { return Environment.Host; }
            set { Environment.Host = value; OnPropertyChanged("Host"); }
        }

        public string Version
        {
            get { return "1"; }
        }

        public string DatabaseType
        {
            get { return Environment.DatabaseType; }
            set { Environment.DatabaseType = value; OnPropertiesChanged("DatabaseType", "IntegratedSecurityEnabled"); }
        }

        public string DatabaseHost
        {
            get { return Environment.DatabaseHost; }
            set { Environment.DatabaseHost = value; OnPropertyChanged("DatabaseHost"); }
        }

        public string DatabaseName
        {
            get { return Environment.DatabaseName; }
            set { Environment.DatabaseName = value; OnPropertyChanged("DatabaseName"); }
        }

        public string DatabaseUsername
        {
            get { return Environment.DatabaseUsername; }
            set { Environment.DatabaseUsername = value; OnPropertyChanged("DatabaseUsername"); }
        }

        public string DatabasePassword
        {
            get { return "*********"; }
            set { Environment.DatabasePassword = value; OnPropertyChanged("DatabasePassword"); }
        }

        public bool IntegratedSecurityEnabled
        {
            get { return !DatabaseType.IsNullOrEmpty() && DatabaseType.Equals("sqlserver"); }
        }

        public string DatabaseVersion
        {
            get { return "2"; }
        }

        public override string ToString()
        {
            return Name.IsNullOrEmpty() ? "Unknown" : Name;
        }
    }
}