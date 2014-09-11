using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataAccess.Sql.ExtensionMethods;
using DataAccess.Sql.Linq.Conditions;
using DataAccess.Sql.Linq.Selections;
using DataAccess.Sql.Statements;

namespace DataAccess.Sql.Linq.Statements
{
    public class SqlQuery : SqlNode, ISelectSource, ISqlStatement
    {
        public bool Distinct { get; private set; }
        public SqlNode Skip { get; private set; }
        public SqlNode Take { get; private set; }
        public FromNode FromNode { get; private set; }
        public SelectNode SelectNode { get; private set; }
        public IList<WhereNode> WhereNodes { get; private set; }
        public IList<OrderNode> OrderNodes { get; private set; }
        public IList<SqlParameter> Parameters { get; private set; }
        private readonly Stack<Action> _actions = new Stack<Action>();

        public string CommandText
        {
            get { return string.Join(" ", SelectClause, FromClause, WhereClause, OrderStatement, SkipAndTake); }
        }

        public string OrderStatement
        {
            get
            {
                return OrderNodes.Any()
                    ? string.Format("order by {0}", string.Join(", ", OrderNodes))
                    : "order by 1";
            }
        }

        public string SkipAndTake
        {
            get
            {
                var builder = new StringBuilder()
                    .AppendFormat("offset ({0}) rows", Skip == null ? "0" : Skip.ToSql());
                if (Take != null)
                {
                    builder.AppendFormat(" fetch next ({0}) rows only", Take.ToSql());
                }
                return builder.ToString();
            }
        }

        public string FromClause { get { return FromNode.ToSql(); } }

        public string SelectClause
        {
            get
            {
                var what = SelectNode == null ? string.Format("{0}.*", FromNode.Alias) : SelectNode.ToSql();
                return string.Format("select {0} {1}", Distinct ? "distinct" : "", what);
            }
        }

        public string WhereClause { get { return WhereNodes.Any() ? string.Format("where {0}", string.Join(" and ", WhereNodes)) : ""; } }

        public bool AllowDefault { get; set; }

        public SqlQuery(Expression expression) : this(expression, null)
        {
        }

        public SqlQuery(Expression expression, SqlNode parent) : base(parent)
        {
            FromNode = new FromNode(this);
            WhereNodes = new List<WhereNode>();
            OrderNodes = new List<OrderNode>();
            Parameters = new List<SqlParameter>();
            Visit(expression);
            while (_actions.Any())
            {
                _actions.Pop()();
            }
        }

        private void Visit(Expression expression)
        {
            DoVisit((dynamic) expression);
        }

        private void DoVisit(MethodCallExpression expression)
        {
            switch (expression.Method.Name)
            {
                case "Where": _actions.Push(() => WhereNodes.Add(new WhereNode(expression.Arguments[1].GetLambda(), FromNode.Alias, this)));
                    Visit(expression.Arguments[0]);
                    break;
                case "OrderBy":
                case "ThenBy":
                    _actions.Push(() => OrderBy(expression.Arguments[1], SortDir.Asc));
                    Visit(expression.Arguments[0]);
                    break;
                case "OrderByDescending":
                case "ThenByDescending":
                    _actions.Push(() => OrderBy(expression.Arguments[1], SortDir.Desc));
                    Visit(expression.Arguments[0]);
                    break;
                case "First":
                case "Single":
                    _actions.Push(() => SelectSingle(expression.GetArgumentOrDefault(1), false));
                    Visit(expression.Arguments[0]);
                    break;
                case "FirstOrDefault":
                case "SingleOrDefault":
                    _actions.Push(() => SelectSingle(expression.GetArgumentOrDefault(1), true));
                    Visit(expression.Arguments[0]);
                    break;
                case "Count":
                    _actions.Push(() => SelectCount(expression.GetArgumentOrDefault(1)));
                    Visit(expression.Arguments[0]);
                    break;
                case "Select":
                    Select(expression);
                    break;
                case "Sum":
                    _actions.Push(() => SelectNode = new SumSelectNode(expression.Arguments[1].GetLambda(), this));
                    Visit(expression.Arguments[0]);
                    break;
                case "Skip":
                    _actions.Push(() => Skip = For(expression.Arguments[1]));
                    Visit(expression.Arguments[0]);
                    break;
                case "Distinct":
                    _actions.Push(() => Distinct = true);
                    Visit(expression.Arguments[0]);
                    break;
                case "Take":
                    _actions.Push(() => Take = For(expression.Arguments[1]));
                    Visit(expression.Arguments[0]);
                    break;
                default : throw new InvalidOperationException(string.Format("Don't know how to parse method {0}", expression.Method.GetFriendlyName()));
            }
        }

        private void Select(MethodCallExpression expression)
        {
            var lambda = expression.Arguments[1].GetLambda();
            SelectNode = new LambdaSelectNode(lambda, FromNode.Alias, this);
            FromNode.Source = new SqlQuery(expression.Arguments[0], this);
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
            throw new InvalidOperationException(string.Format("Don't know how to visit {0}", invalid.GetType().GetFriendlyName()));
        }

        public void SelectCount(Expression condition)
        {
            SelectNode = new CountSelectNode(this);
            if (condition != null)
            {
                WhereNodes.Add(new WhereNode(condition.GetLambda(), FromNode.Alias, this));
            }
        }

        public void SelectSingle(Expression condition, bool allowDefault)
        {
            AllowDefault = allowDefault;
            Take = new ConstantNode(Expression.Constant(1), this);
            if (condition != null)
            {
                WhereNodes.Add(new WhereNode(condition.GetLambda(), FromNode.Alias, this));
            }
        }

        public void OrderBy(Expression expression, SortDir direction)
        {
            OrderNodes.Add(new OrderNode(expression.GetLambda(), direction, FromNode.Alias, this));
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

        public override SqlParameter GetParameterFor(object value)
        {
            if (Parent != null)
            {
                return base.GetParameterFor(value);
            }
            var parameter = new SqlParameter(NextParameterName(), value);
            Parameters.Add(parameter);
            return parameter;
        }

        private string NextParameterName()
        {
            return string.Format("@p{0}", Parameters.Count);
        }

        public void Where(LambdaExpression lambda)
        {
            WhereNodes.Add(new WhereNode(lambda, FromNode.Alias, this));
        }

        public string Sql { get { return string.Format("({0})", ToSql()); } }
    }
}