using BasicManifest.Core.Domain;

namespace BasicManifest.UnitTesting.Builders
{
    public class GroupBuilder : DomainBuilderBase<GroupBuilder, LoadGroup>
    {
        public GroupBuilder()
            : this(WithId(new LoadGroup()))
        {
        }

        public GroupBuilder(LoadGroup item) : base(item)
        {
        }

        public GroupBuilder WithJumper(Person jumper)
        {
            return WithSlot(new Slot(jumper));
        }

        public GroupBuilder WithSlot(Slot slot)
        {
            Item.Add(slot);
            return MySelf;
        }

        public GroupBuilder ForLoad(Load load)
        {
            load.Add(Item);
            return MySelf;
        }
    }
}