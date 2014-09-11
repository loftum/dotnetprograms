using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Sql.ExtensionMethods;
using DataAccess.Sql.Linq.Conditions;

namespace DataAccess.Sql.Linq.Statements
{
    public class SqlDelete : SqlNode, ISqlStatement
    {
        public FromNode FromNode { get; private set; }
        public IList<WhereNode> WhereNodes { get; private set; }
        public IList<SqlParameter> Parameters { get; private set; }

        public string CommandText { get { return string.Join(" ", DeleteClause, WhereClause); } }

        public string DeleteClause
        {
            get { return string.Format("delete {0} {1}", FromNode.Alias, FromNode.ToSql()); }
        }

        public string WhereClause
        {
            get
            {
                return WhereNodes.Any()
                    ? string.Format("where {0}", string.Join(" and ", WhereNodes))
                    : string.Empty;
            }
        }

        public SqlDelete(Expression expression) : base(null)
        {
            FromNode = new FromNode(this);
            WhereNodes = new List<WhereNode>();
            Parameters = new List<SqlParameter>();
            Visit(expression);
        }

        public override SqlParameter GetParameterFor(object value)
        {
            if (Parent != null)
            {
                return base.GetParameterFor(value);
            }
            var existing = Parameters.FirstOrDefault(p => p.Value == value);
            if (existing != null)
            {
                return existing;
            }
            var parameter = new SqlParameter(NextParameterName(), value);
            Parameters.Add(parameter);
            return parameter;
        }

        private string NextParameterName()
        {
            return string.Format("@p{0}", Parameters.Count);
        }

        private void Visit(Expression expression)
        {
            DoVisit((dynamic) expression);
        }

        private void DoVisit(MethodCallExpression expression)
        {
            switch (expression.Method.Name)
            {
                case "Where":
                    WhereNodes.Add(new WhereNode(expression.Arguments[1].GetLambda(), FromNode.Alias, this));
                    Visit(expression.Arguments[0]);
                    break;
                case "OrderBy":
                case "OrderByDescending":
                case "ThenBy":
                case "ThenByDescending":
                    Visit(expression.Arguments[0]);
                    break;
                default:
                    throw new InvalidOperationException(string.Format("Method {0} is not valid for {1}", expression.Method.GetFriendlyName(), GetType().GetFriendlyName()));
            }
        }

        private void DoVisit(ConstantExpression expression)
        {
            if (typeof(IQueryable).IsAssignableFrom(expression.Type))
            {
                var name = expression.Type.GenericTypeArguments[0].Name;
                FromNode.Source = new TableSource(name);
            }
            else
            {
                throw new InvalidOperationException("Uh...what?");
            }
        }

        private static void DoVisit(object invalid)
        {
            throw new InvalidOperationException(string.Format("Don't know how to parse a {0}", invalid.GetType().GetFriendlyName()));
        }

        public override string ToSql()
        {
            return CommandText;
        }

        public override string ToString()
        {
            if (!Parameters.Any())
            {
                return CommandText;
            }
            var parameterNameAndValues = string.Join(", ", Parameters.Select(p => string.Format("{0}={1}", p.ParameterName, p.Value ?? "null")));
            return string.Format("{0} ({1})", CommandText, parameterNameAndValues);
        }
    }
}