using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MissingLinq.Sql.Interpreters.Tree.Methods;

namespace MissingLinq.Sql.Interpreters.Tree
{
    public abstract class TreeNode : ITreeNode
    {
        public MissingLinqSql Sql { get; private set; }

        protected TreeNode(MissingLinqSql sql)
        {
            Sql = sql;
        }

        public abstract string Translate();

        public static ITreeNode For(MissingLinqSql parent, Expression expression)
        {
            return TreeNodeFor(parent, (dynamic)expression);
        }

        public static IList<ITreeNode> For(MissingLinqSql parent, IEnumerable<Expression> expressions)
        {
            return expressions.Select(e => For(parent, e)).ToList();
        }

        public static IList<ITreeNode> For(MissingLinqSql parent, params Expression[] expressions)
        {
            return expressions.Select(e => For(parent, e)).ToList();
        }

        private static ConstantNode TreeNodeFor(MissingLinqSql sql, ConstantExpression constant)
        {
            return new ConstantNode(sql, constant);
        }

        private static BinaryNode TreeNodeFor(MissingLinqSql sql, BinaryExpression binary)
        {
            return new BinaryNode(sql, binary);
        }

        private static ParameterNode TreeNodeFor(MissingLinqSql sql, ParameterExpression parameter)
        {
            return new ParameterNode(sql, parameter);
        }

        protected static LambdaNode TreeNodeFor(MissingLinqSql sql, LambdaExpression lambda)
        {
            return new LambdaNode(sql, lambda);
        }

        private static ITreeNode TreeNodeFor(MissingLinqSql sql, UnaryExpression unary)
        {
            return new UnaryNode(sql, unary);
        }
        
        private static MemberNode TreeNodeFor(MissingLinqSql sql, MemberExpression member)
        {
            return new MemberNode(sql, member);
        }

        private static ITreeNode TreeNodeFor(MissingLinqSql sql, MethodCallExpression method)
        {
            switch (method.Method.Name)
            {
                case "Select":
                    return new SelectNode(sql, method);
                case "Where":
                    return new WhereNode(sql, method);
                case "Take":
                    return new TakeNode(sql, method);
                case "OrderBy":
                case "ThenBy":
                    return new OrderByNode(sql, method);
                case "OrderByDescending":
                case "ThenByDescending":
                    return new OrderByNode(sql, method, false);
                case "Contains":
                    return new ContainsNode(sql, method);
                case "StartsWith":
                    return new StartsWithNode(sql, method);
                case "EndsWith":
                    return new EndsWithNode(sql, method);
                case "Single":
                case "SingleOrDefault":
                case "First":
                case "FirstOrDefault":
                    return new FirstNode(sql, method);
                case "Count":
                    return new CountNode(sql, method);
                default:
                throw new InvalidOperationException(string.Format("Method not supported: {0}", method.Method.Name));
            }
        }

        private static ITreeNode TreeNodeFor(MissingLinqSql sql, object invalid)
        {
            throw new InvalidOperationException(string.Format("Don't know how to handle {0}", invalid.GetType()));
        }

        public override string ToString()
        {
            return Translate();
        }
    }
}