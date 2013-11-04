using System.Collections.Generic;
using System.Linq;

namespace MasterData.Web.Menus
{
    public class MenuBar
    {
        public static MenuBar Top { get; set; }

        private readonly IDictionary<string, Menu> _menus = new Dictionary<string, Menu>();
        public IEnumerable<Menu> Menus { get { return _menus.Values.OrderBy(m => m.Order).ThenBy(m => m.Name); } }

        public Menu this[string name]
        {
            get
            {
                if (!_menus.ContainsKey(name))
                {
                    _menus[name] = new Menu(name);
                }
                return _menus[name];
            }
        }
    }
}