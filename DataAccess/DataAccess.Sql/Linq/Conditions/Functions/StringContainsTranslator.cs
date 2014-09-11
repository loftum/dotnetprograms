namespace DataAccess.Sql.Linq.Conditions.Functions
{
    public class StringContainsTranslator : IMethodTranslator
    {
        public string Translate(MethodNode method)
        {
            return string.Format("{0} like '%' + {1} +'%'", method.Object, method.Arguments[0]);
        }
    }
}