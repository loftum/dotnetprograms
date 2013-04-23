using System;
using BasicManifest.Core.Domain;

namespace BasicManifest.UnitTesting.Builders
{
    public class SkydiverBuilder : DomainBuilderBase<SkydiverBuilder, Skydiver>
    {
        public SkydiverBuilder() : this(WithId(new Skydiver()))
        {
        }

        public SkydiverBuilder(Skydiver item) : base(item)
        {
        }

        public SkydiverBuilder WithRole(PersonRole role)
        {
            Item.Role = role;
            return MySelf;
        }

        public SkydiverBuilder WithMoney(decimal amount)
        {
            Item.Account.Insert(amount);
            return MySelf;
        }

        public SkydiverBuilder WithFirstName(string firstName)
        {
            Item.FirstName = firstName;
            return MySelf;
        }

        public SkydiverBuilder WithLastName(string lastName)
        {
            Item.LastName = lastName;
            return MySelf;
        }

        public Skydiver WithBirthDate(DateTime birthDate)
        {
            Item.BirthDate = birthDate;
            return MySelf;
        }
    }
}