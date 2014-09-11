using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Sql.ExtensionMethods;
using DataAccess.Sql.Linq.Conditions.Functions;

namespace DataAccess.Sql.Linq.Conditions
{
    public abstract class SqlNode
    {
        private static readonly TranslationProvider Provider = new TranslationProvider();

        static SqlNode()
        {
            Provider.Map<string>(s => s.Contains("whatever"), new StringContainsTranslator());
            Provider.Map<string>(s => s.StartsWith("whatever"), n => string.Format("{0} like '' + {1} +'%'", n.Object, n.Arguments[0]));
            Provider.Map<string>(s => s.EndsWith("whatever"), n => string.Format("{0} like '%' + {1} + ''", n.Object, n.Arguments[0]));
            Provider.Map<string>(s => s.ToUpper(), n => string.Format("upper({0})", n.Object));
            Provider.Map<string>(s => s.Equals(""), n => string.Format("{0} = {1}", n.Object, n.Arguments[0]));
        }

        public virtual SqlParameter GetParameterFor(object value)
        {
            return Parent.GetParameterFor(value);
        }

        public virtual string GetAliasFor(string lambdaParameter)
        {
            return Parent == null ? null : Parent.GetAliasFor(lambdaParameter);
        }

        private int _aliasCounter;
        protected string NextAlias()
        {
            return Parent == null
                ? string.Format("x{0}", _aliasCounter++)
                : Parent.NextAlias();
        }

        protected readonly SqlNode Parent;

        protected SqlNode(SqlNode parent)
        {
            Parent = parent;
        }

        protected SqlNode For(Expression expression)
        {
            return CreateFor((dynamic)expression);
        }

        private SqlNode CreateFor(LambdaExpression expression)
        {
            return new LambdaNode(expression, this);
        }

        private SqlNode CreateFor(UnaryExpression expression)
        {
            return For(expression.Operand);
        }

        private SqlNode CreateFor(BinaryExpression expression)
        {
            return new BinaryNode(expression, this);
        }

        private SqlNode CreateFor(MemberExpression expression)
        {
            if (expression.Expression is ParameterExpression)
            {
                return new ColumnNode(expression, this);
            }
            return new SqlParameterNode(expression, this);
        }

        private SqlNode CreateFor(MemberInitExpression expression)
        {
            return new ColumnSelectorNode(expression, this);
        }

        private SqlNode CreateFor(ConstantExpression expression)
        {
            return new ConstantNode(expression, this);
        }

        private SqlNode CreateFor(NewExpression expression)
        {
            if (expression.Type.IsValueOrString())
            {
                return new NewSimpleValueNode(expression, this);
            }
            return new ColumnSelectorNode(expression, this);
        }

        private SqlNode CreateFor(ParameterExpression expression)
        {
            
            return new AliasNode(expression, this);
        }

        private SqlNode CreateFor(MethodCallExpression expression)
        {
            var translator = Provider.Get(expression.Method);
            if (translator == null)
            {
                throw new InvalidOperationException(string.Format("Don't know how to handle {0}", expression.Method.GetFriendlyName()));
            }
            return new MethodNode(expression, this, translator);
        }

        private static SqlNode CreateFor(object invalid)
        {
            throw new InvalidOperationException(string.Format("Don't know how to handle {0}", invalid.GetType().GetFriendlyName()));
        }

        public abstract string ToSql();

        public override string ToString()
        {
            return ToSql();
        }
    }
}