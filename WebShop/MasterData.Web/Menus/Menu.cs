using System.Collections.Generic;
using System.Linq;

namespace MasterData.Web.Menus
{
    public class Menu
    {
        public string Name { get; set; }
        private readonly IDictionary<string, MenuItem> _items = new Dictionary<string, MenuItem>();
        public IEnumerable<MenuItem> Items { get { return _items.Values.OrderBy(i => i.Order); } }

        public int Order { get; set; }

        public Menu(string name)
        {
            Name = name;
        }

        public void Add(MenuItem menuItem)
        {
            if (_items.ContainsKey(menuItem.Name))
            {
                return;
            }
            _items[menuItem.Name] = menuItem;
        }
    }
}