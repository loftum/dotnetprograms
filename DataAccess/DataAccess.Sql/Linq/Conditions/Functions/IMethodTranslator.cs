namespace DataAccess.Sql.Linq.Conditions.Functions
{
    public interface IMethodTranslator
    {
        string Translate(MethodNode node);
    }
}