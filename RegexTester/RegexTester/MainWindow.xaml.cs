using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using RegexTester.RegexOperations;

namespace RegexTester
{
    public partial class MainWindow
    {
        public RegexModel Model{ get; set;}

        private readonly IRegexValidator _regexValidator;
        private readonly IRegexMatcher _matcher;

        public MainWindow()
        {
            InitializeComponent();
            _regexValidator = new RegexValidator();
            _matcher = new RegexMatcher();
            Model = new RegexModel();
        }

        private void Pattern_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidatePatternText(_regexValidator.IsValid(Pattern.Text));
        }

        private void ValidatePatternText(bool valid)
        {
            if (valid)
            {
                Pattern.Background = Brushes.AliceBlue;
                Status.Text = string.Empty;
            }
            else
            {
                Pattern.Background = Brushes.Beige;
            }
        }

        private void MatchButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var pattern = Pattern.Text;
            if (_regexValidator.IsValid(pattern))
            {
                var input = Input.Text;
                var matches = _matcher.Match(pattern, input);
                Matches.Text = matches.ToString();
            }
            else
            {
                Status.Text = _regexValidator.ValidationMessageFor(pattern);
            }
        }
    }
}
