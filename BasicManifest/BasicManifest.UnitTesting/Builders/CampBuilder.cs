using BasicManifest.Core.Domain;

namespace BasicManifest.UnitTesting.Builders
{
    public class CampBuilder : DomainBuilderBase<CampBuilder, Camp>
    {
        public CampBuilder() : this(WithId(new Camp()))
        {
        }

        public CampBuilder(Camp item) : base(item)
        {
        }

        public CampBuilder WithDay(Day day)
        {
            Item.Add(day);
            return MySelf;
        }

        public CampBuilder WithParticipant(Skydiver skydiver)
        {
            Item.Add(skydiver);
            return MySelf;
        }

        public CampBuilder WithDefaultSlotPrice(decimal slotPrice)
        {
            Item.DefaultSlotPrice = slotPrice;
            return MySelf;
        }

        public CampBuilder WithMoney(decimal amount)
        {
            Item.Account.Insert(amount);
            return MySelf;
        }
    }
}