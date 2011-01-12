using HourGlass.Lib.Domain;

namespace HourGlass.UnitTesting.Builders
{
    public class HourCodeBuilder : BuilderBase<HourCodeBuilder, HourCode>
    {
        public HourCodeBuilder(HourCode item) : base(item)
        {
        }

        public static HourCodeBuilder NewHourCode()
        {
            var hourCode = new HourCode();
            return new HourCodeBuilder(hourCode);
        }

        public HourCodeBuilder WithCode(string code)
        {
            Item.Code = code;
            return this;
        }

        public HourCodeBuilder WithName(string name)
        {
            Item.Name = name;
            return this;
        }
    }
}