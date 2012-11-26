using System;
using System.Linq;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq
{
    public class QueryableToSqlTranslator : IQueryableToSqlTranslator
    {
        private readonly DbToolSql _sql;


        public QueryableToSqlTranslator()
        {
            _sql = new DbToolSql();
        }

        public DbToolSql Translate(IQueryable queryable)
        {
            var expression = queryable.Expression;
            Visit((dynamic)expression);
            return _sql;
        }

        private void Visit(ConstantExpression expression)
        {
            
        }

        private void Visit(MethodCallExpression expression)
        {
            switch (expression.Method.Name)
            {
                case "Select":
                    _sql.Append(new Selector(expression).GetSql());
                    break;
                case "Where":
                    _sql.Append(new Wherer(expression).GetSql());
                    break;
            }
            Visit((dynamic)expression.Arguments[0]);
        }

        private void Visit(object invalid)
        {
            throw new NotImplementedException(string.Format("Don't know what to with {0}", invalid.GetType()));
        }

        private static string GetSelectFor(IQueryable queryable)
        {
            var tableName = queryable.ElementType.Name;
            return string.Format("select * from {0}", tableName);
        }
    }
}