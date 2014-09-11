using System;

namespace DataAccess.Sql.Linq.Conditions.Functions
{
    public class MethodTranslator : IMethodTranslator
    {
        private readonly Func<MethodNode, string> _translate;

        public MethodTranslator(Func<MethodNode, string> translate)
        {
            _translate = translate;
        }

        public string Translate(MethodNode node)
        {
            return _translate(node);
        }
    }
}