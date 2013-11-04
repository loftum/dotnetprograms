using System;

namespace MasterData.Web.Menus
{
    public class MenuAttributeBase : Attribute
    {
        public MenuAttributeBase(string name, int order)
        {
            Name = name;
            Order = order;
        }

        public MenuAttributeBase(int order)
        {
            Order = order;
        }

        public MenuAttributeBase(string name)
        {
            Name = name;
            Order = int.MaxValue;
        }

        public MenuAttributeBase()
        {
            Order = int.MaxValue;
        }

        public string Name { get; private set; }
        public int Order { get; private set; }

        public bool HasName
        {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }
    }
}