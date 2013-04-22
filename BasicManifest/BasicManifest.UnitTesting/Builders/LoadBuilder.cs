using BasicManifest.Core.Domain;

namespace BasicManifest.UnitTesting.Builders
{
    public class LoadBuilder : DomainBuilderBase<LoadBuilder, Load>
    {
        public LoadBuilder() : this(WithId(new Load()))
        {
        }

        public LoadBuilder(Load item) : base(item)
        {
        }

        public LoadBuilder WithGroup(LoadGroup loadGroup)
        {
            Item.Add(loadGroup);
            return MySelf;
        }

        public LoadBuilder ForDay(Day day)
        {
            day.Add(Item);
            return MySelf;
        }
    }
}