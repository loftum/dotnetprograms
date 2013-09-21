using System;
using System.Linq.Expressions;
using DotNetPrograms.Common.ExtensionMethods;
using FluentNHibernate.Mapping;
using WebShop.Core.Domain.Inheritance;

namespace WebShop.Core.Domain.MasterData.Mappings
{
    public abstract class MasterDataObjectMap<T> : ClassMap<T> where T : MasterDataObject
    {
        protected MasterDataObjectMap()
        {
            Id(o => o.Id).GeneratedBy.Guid();
        }

        protected void MapInheritable<TProperty>(Expression<Func<T, Inheritable<TProperty>>> property, string columnName)
        {
            Component(property, p => MapInheritableComponent(p, columnName));
        }

        protected void MapInheritable<TProperty>(Expression<Func<T, Inheritable<TProperty>>> property)
        {
            Component(property, p => MapInheritableComponent(p, property.GetMemberName()));
        }

        private static void MapInheritableComponent<TProperty>(ComponentPart<Inheritable<TProperty>> m, string columnName)
        {
            m.Map(i => i.Value, columnName);
        }
    }
}