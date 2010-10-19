using System.Collections.Generic;
using System.Windows.Input;
using DeployWizard.Lib.Events.Profile;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSelectProfileStepView : ISelectProfileStepView
    {
        public event NewProfileEventHandler NewProfile;
        public event DeleteProfileEventHandler DeleteProfile;

        private IEnumerable<string> _profiles = new string[0];


        public IEnumerable<string> Profiles
        {
            get
            {
                return _profiles;
            }
            set
            {
                _profiles = value;
                ProfileCombobox.ItemsSource = _profiles;
            }
        }

        public string SelectedProfile
        {
            get { return ProfileCombobox.SelectedValue == null ? string.Empty : ProfileCombobox.SelectedValue.ToString(); }
            set { ProfileCombobox.SelectedItem = value; }
        }

        public WpfSelectProfileStepView()
        {
            InitializeComponent();
        }

        private void CreateNewButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            InvokeNewProfile(sender);
        }

        private void NewProfileName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                InvokeNewProfile(sender);
            }
        }

        private void InvokeNewProfile(object sender)
        {
            NewProfile(sender, new ProfileEventHandlerArgs(NewProfileName.Text));
            NewProfileName.Text = string.Empty;
        }

        public void ValidateAll()
        {
            
        }

        private void DeleteProfileButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DeleteProfile(sender, new ProfileEventHandlerArgs(SelectedProfile));
        }
    }
}
