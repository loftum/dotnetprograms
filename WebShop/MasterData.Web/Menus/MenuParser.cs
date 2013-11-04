using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using DotNetPrograms.Common.Meta;

namespace MasterData.Web.Menus
{
    public class MenuParser
    {
        private readonly Assembly _assembly;

        public MenuParser(Assembly assembly)
        {
            _assembly = assembly;
        }

        public MenuBar GetMenuBar()
        {
            var types = _assembly.GetTypes().Where(t => typeof(Controller).IsAssignableFrom(t));
            var controllerTypes = types
                .Select(t => new TypeMeta(t))
                .Where(t => t.HasCustomAttribute<MenuAttribute>());

            var menuBar = new MenuBar();
            foreach (var controllerType in controllerTypes)
            {
                var controllerName = controllerType.Type.Name.Replace("Controller", "");
                var menuAttribute = controllerType.GetCustomAttribute<MenuAttribute>();
                var menuName = menuAttribute.HasName ? menuAttribute.Name : controllerName;
                var menu = menuBar[menuName];
                menu.Order = menuAttribute.Order;
                foreach (var method in controllerType.Methods.Where(m => m.HasCustomAttribute<MenuItemAttribute>()))
                {
                    var actionName = method.Name;
                    if (method.HasCustomAttribute<ActionNameAttribute>())
                    {
                        actionName = method.GetCustomAttribute<ActionNameAttribute>().Name;
                    }
                    var itemAttribute = method.GetCustomAttribute<MenuItemAttribute>();
                    var itemName = itemAttribute.HasName ? itemAttribute.Name : actionName;
                    
                    menu.Add(new MenuItem(itemName, controllerName, actionName, itemAttribute.Order));
                }
            }
            return menuBar;
        }
    }
}