using BasicManifest.Core.Domain;

namespace BasicManifest.UnitTesting.Builders
{
    public class DayBuilder : DomainBuilderBase<DayBuilder, Day>
    {
        public DayBuilder() : this(WithId(new Day()))
        {
        }

        public DayBuilder(Day item) : base(item)
        {
        }

        public DayBuilder WithLoad(Load load)
        {
            Item.Add(load);
            return MySelf;
        }

        public DayBuilder ForCamp(Camp camp)
        {
            camp.Add(Item);
            return MySelf;
        }
    }
}