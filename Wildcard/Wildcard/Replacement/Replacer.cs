using Wildcard.Readers;

namespace Wildcard.Replacement
{
    public class Replacer
    {
        private readonly ReplacementParameters _parameters;

        public Replacer(ReplacementParameters parameters)
        {
            _parameters = parameters;
        }

        public void Replace()
        {
            var wildcards = new WildcardFileReader().GetWildcards(_parameters.WildcardFilePath);
             
        }
    }
}
