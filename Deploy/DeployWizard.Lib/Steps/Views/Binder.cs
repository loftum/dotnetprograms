using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace DeployWizard.Lib.Steps.Views
{
    public class Binder
    {
        private readonly Binding _binding;

        private Binder(object source, string path)
        {
            _binding = new Binding
            {
                Source = source,
                Path = new PropertyPath(path)
            };
        }

        public static Binder Bind(object source, string path)
        {
            return new Binder(source, path);
        }

        public Binder WithTargetNullValue(object value)
        {
            _binding.TargetNullValue = value;
            return this;
        }

        public Binder ToTextBox(TextBox textBox)
        {
            textBox.SetBinding(TextBox.TextProperty, _binding);
            return this;
        }

        public Binder ToCheckBox(CheckBox checkBox)
        {
            checkBox.SetBinding(ToggleButton.IsCheckedProperty, _binding);
            return this;
        }

        public void ToTextBlock(TextBlock textBlock)
        {
            textBlock.SetBinding(TextBlock.TextProperty, _binding);
        }

        public void ToComboBox(ComboBox comboBox)
        {
            comboBox.ItemsSource = new[] {_binding};
            //comboBox.SetBinding(ComboBox.ItemsSourceProperty, _binding);
        }
    }
}
