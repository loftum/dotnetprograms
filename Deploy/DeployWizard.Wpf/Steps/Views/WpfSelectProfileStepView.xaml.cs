﻿using System.Collections.Generic;
using System.Windows.Input;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSelectProfileStepView : ISelectProfileStepView
    {
        public event NewProfileEventHandler NewProfile;

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
            get { return ProfileCombobox.SelectedValue.ToString(); }
            set { ProfileCombobox.SelectedItem = value; }
        }

        public WpfSelectProfileStepView()
        {
            InitializeComponent();
        }

        private void CreateNewButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NewProfile.Invoke(sender, new NewProfileEventHandlerArgs(NewProfileName.Text));
        }

        private void NewProfileName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NewProfile.Invoke(sender, new NewProfileEventHandlerArgs(NewProfileName.Text));
            }
        }
    }
}
