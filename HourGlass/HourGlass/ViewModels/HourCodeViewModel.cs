using System.Windows.Input;
using HourGlass.Commands;
using HourGlass.Lib.Domain;
using HourGlass.Providers;

namespace HourGlass.ViewModels
{
    public class HourCodeViewModel : ViewModelBase
    {
        public ICommand SaveCommand { get; private set; }
        public HourCode HourCode{get; private set;}
        private readonly IHourCodeProvider _hourCodeProvider;

        public HourCodeViewModel(IHourCodeProvider hourCodeProvider, HourCode hourCode)
        {
            HourCode = hourCode;
            _hourCodeProvider = hourCodeProvider;
            SaveCommand = new DelegateCommand(Save);
        }

        private void Save(object obj)
        {
            if (HourCode != null)
            {
                _hourCodeProvider.Save(this);
            }
        }

        public bool CanDelete
        {
            get
            {
                return !HourCode.InUse;
            }
        }

        public string Code
        {
            get
            {
                return HourCode.Code;
            }
            set
            {
                HourCode.Code = value;
                OnPropertyChanged("Code");
            }
        }

        public string Name
        {
            get { return HourCode.Name; }
            set
            {
                HourCode.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public double Usage
        {
            get
            {
                return HourCode.Usage;
            }
        }

        public override string ToString()
        {
            return HourCode == null ?
                string.Empty :
                HourCode.ToString();
        }
    }
}