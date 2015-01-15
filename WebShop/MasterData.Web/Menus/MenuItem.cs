namespace MasterData.Web.Menus
{
    public class MenuItem
    {
        public string Name { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public int Order { get; private set; }

        public MenuItem(string name, string controller, string action, int order)
        {
            Action = action;
            Controller = controller;
            Name = name;
            Order = order;
        }
    }
}