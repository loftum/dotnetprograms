using BasicManifest.Core.Domain;

namespace BasicManifest.UnitTesting.Builders
{
    public class PersonBuilder : DomainBuilderBase<PersonBuilder, Person>
    {
        public PersonBuilder() : this(WithId(new Person()))
        {
        }

        public PersonBuilder(Person item) : base(item)
        {
        }

        public PersonBuilder WithRole(PersonRole role)
        {
            Item.Role = role;
            return MySelf;
        }

        public PersonBuilder WithMoney(decimal amount)
        {
            Item.Account.Insert(amount);
            return MySelf;
        }
    }
}