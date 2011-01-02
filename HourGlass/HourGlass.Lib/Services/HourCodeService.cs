using System.Collections.ObjectModel;
using System.Linq;
using HourGlass.Lib.Data;
using HourGlass.Lib.Domain;
using HourGlass.Lib.Exceptions;

namespace HourGlass.Lib.Services
{
    public class HourCodeService : IHourCodeService
    {
        public ObservableCollection<HourCode> HourCodes { get; private set; }
        private readonly IHourGlassRepo _repo;

        public HourCodeService(IHourGlassRepo repo)
        {
            _repo = repo;
            HourCodes = new ObservableCollection<HourCode>();
            foreach (var hourCode in repo.GetAll<HourCode>())
            {
                HourCodes.Add(hourCode);
            }
        }

        public HourCode AddHourCode(string code, string name)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))
            {
                throw new HourGlassUserException(ExceptionType.InvalidHourCode, code, name);
            }

            var hourCode = new HourCode {Code = code.Trim(), Name = name.Trim()};
            var duplicate = FindDuplicateOf(hourCode);
            if (duplicate != null)
            {
                return duplicate;
            }
            
            _repo.Save(hourCode);
            HourCodes.Add(hourCode);
            return hourCode;
        }

        public HourCode Save(HourCode hourCode)
        {
            return _repo.Save(hourCode);
        }

        public HourCode Remove(HourCode hourCode)
        {
            return _repo.Delete(hourCode);
        }

        private HourCode FindDuplicateOf(HourCode hourCode)
        {
            return HourCodes
                .FirstOrDefault(code => 
                    code.Code.Equals(hourCode.Code) &&
                    code.Name.Equals(hourCode.Name));
        }
    }
}