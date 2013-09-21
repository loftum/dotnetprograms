using System;
using System.Collections.Generic;
using WebShop.Core.Domain.Inheritance;

namespace WebShop.Core.Domain.MasterData
{
    public class Product : MasterDataObject
    {
        public virtual string Level { get { return Parent == null ? "Master" : "Variant"; } }
        public virtual Product Parent { get; set; }
        public virtual IList<Product> Children { get; set; }
        public virtual string Name { get; set; }
        public virtual Price Price { get; set; }

        private Inheritable<string> _description;
        public virtual Inheritable<string> Description
        {
            get { return _description ?? (_description = new Inheritable<string>(() => Level, () => FromParent(p => p.Description))); }
            protected set { _description = value; }
        }

        public virtual string ProductNumber { get; set; }

        public Product()
        {
            Price = Price.Zero;
            Children = new List<Product>();
        }

        private Inheritable<T> FromParent<T>(Func<Product, Inheritable<T>> property)
        {
            return Parent == null ? null : property(Parent);
        }

        public virtual void Add(Product child)
        {
            Children.Add(child);
            child.Parent = this;
        }
    }
}