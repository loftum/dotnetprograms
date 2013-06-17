using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DbTool.Lib.Linq.Interpreters.Tree.Methods;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public abstract class TreeNode : ITreeNode
    {
        public DbToolSql Sql { get; private set; }

        protected TreeNode(DbToolSql sql)
        {
            Sql = sql;
        }

        public abstract string Translate();

        public static ITreeNode For(DbToolSql parent, Expression expression)
        {
            return TreeNodeFor(parent, (dynamic)expression);
        }

        public static IList<ITreeNode> For(DbToolSql parent, IEnumerable<Expression> expressions)
        {
            return expressions.Select(e => For(parent, e)).ToList();
        }

        public static IList<ITreeNode> For(DbToolSql parent, params Expression[] expressions)
        {
            return expressions.Select(e => For(parent, e)).ToList();
        }

        private static ConstantNode TreeNodeFor(DbToolSql sql, ConstantExpression constant)
        {
            return new ConstantNode(sql, constant);
        }

        private static BinaryNode TreeNodeFor(DbToolSql sql, BinaryExpression binary)
        {
            return new BinaryNode(sql, binary);
        }

        private static ParameterNode TreeNodeFor(DbToolSql sql, ParameterExpression parameter)
        {
            return new ParameterNode(sql, parameter);
        }

        protected static LambdaNode TreeNodeFor(DbToolSql sql, LambdaExpression lambda)
        {
            return new LambdaNode(sql, lambda);
        }

        private static ITreeNode TreeNodeFor(DbToolSql sql, UnaryExpression unary)
        {
            return new UnaryNode(sql, unary);
        }
        
        private static MemberNode TreeNodeFor(DbToolSql sql, MemberExpression member)
        {
            return new MemberNode(sql, member);
        }

        private static ITreeNode TreeNodeFor(DbToolSql sql, MethodCallExpression method)
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
                default:
                throw new InvalidOperationException(string.Format("Method not supported: {0}", method.Method.Name));
            }
        }

        private static ITreeNode TreeNodeFor(DbToolSql sql, object invalid)
        {
            throw new InvalidOperationException(string.Format("Don't know how to handle {0}", invalid.GetType()));
        }

        public override string ToString()
        {
            return Translate();
        }
    }
}