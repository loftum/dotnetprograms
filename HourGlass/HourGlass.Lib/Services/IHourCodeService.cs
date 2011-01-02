using System.Collections.ObjectModel;
using HourGlass.Lib.Domain;

namespace HourGlass.Lib.Services
{
    public interface IHourCodeService
    {
        ObservableCollection<HourCode> HourCodes { get; }
        HourCode AddHourCode(string code, string name);
    }
}