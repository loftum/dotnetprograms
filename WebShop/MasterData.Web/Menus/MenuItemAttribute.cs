namespace MasterData.Web.Menus
{
    public class MenuItemAttribute : MenuAttributeBase
    {
        public MenuItemAttribute(string name, int order) : base(name, order)
        {
        }

        public MenuItemAttribute(int order) : base(order)
        {
        }

        public MenuItemAttribute(string name) : base(name)
        {
        }

        public MenuItemAttribute()
        {
        }
    }
}