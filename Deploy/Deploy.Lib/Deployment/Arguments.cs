using System.Collections.Generic;
using System.Linq;

namespace Deploy.Lib.Deployment
{
    public class Arguments
    {
        private readonly IEnumerable<string> _args;

        public Arguments(IEnumerable<string> args)
        {
            _args = args;
        }

        public string ByNameOrIndex(string name, int index)
        {
            var byName = GetByName(name);
            return string.IsNullOrEmpty(byName)
                       ? _args.ElementAt(index)
                       : byName;
        }

        private string GetByName(string name)
        {
            foreach (var arg in _args.Where(arg => arg.StartsWith(name)))
            {
                return arg.Substring(name.Length + 1);
            }
            return string.Empty;
        }
    }
}
