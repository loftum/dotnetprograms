namespace MasterData.Web.Menus
{
    public class MenuAttribute : MenuAttributeBase
    {
        public MenuAttribute(string name, int order) : base(name, order)
        {
        }

        public MenuAttribute(int order) : base(order)
        {   
        }

        public MenuAttribute(string name) : base(name)
        {
        }

        public MenuAttribute()
        {
        }
    }
}