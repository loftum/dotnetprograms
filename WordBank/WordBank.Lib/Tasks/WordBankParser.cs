using System;
using System.Collections.Generic;
using System.Linq;
using Wordbank.Lib.ExtensionMethods;
using Wordbank.Lib.Logging;

namespace WordBank.Lib.Tasks
{
    public class WordBankParser : IWordBankParser
    {
        private readonly IWordBankLogger _logger;

        public WordBankParser(IWordBankLogger logger)
        {
            _logger = logger;
        }

        public T Parse<T>(IList<string> values) where T : class, new()
        {
            var paradigme = new T();
            var type = typeof(T);
            var properties = type.GetProperties()
                .Where(p => !p.Name.In("Id", "Ords", "Paradigmes"))
                .ToArray();
            for(var ii=0; ii<values.Count; ii++)
            {
                var propertyType = properties[ii].PropertyType;
                try
                {
                    properties[ii].SetValue(paradigme, values[ii].ConvertTo(propertyType), new object[0]);
                }
                catch (Exception)
                {
                    _logger.Error("Could not set {0} to {1}", properties[ii].Name, values[ii]);
                    throw;
                }
            }
            return paradigme;
        }
    }
}