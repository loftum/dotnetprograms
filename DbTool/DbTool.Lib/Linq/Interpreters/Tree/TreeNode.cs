using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DbTool.Lib.Linq.ExtensionMethods;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public abstract class TreeNode : ITreeNode
    {
        protected bool HasParent { get { return Parent != null; } }
        protected ITreeNode Parent;

        protected TreeNode(ITreeNode parent)
        {
            Parent = parent;
        }

        public abstract string Translate();

        public static ITreeNode For(ITreeNode parent, Expression expression)
        {
            return TreeNodeFor(parent, (dynamic)expression);
        }

        public static IList<ITreeNode> For(ITreeNode parent,IEnumerable<Expression> expressions)
        {
            return expressions.Select(e => For(parent, e)).ToList();
        }

        public static IList<ITreeNode> For(ITreeNode parent, params Expression[] expressions)
        {
            return expressions.Select(e => For(parent, e)).ToList();
        }

        private static ConstantNode TreeNodeFor(ITreeNode parent, ConstantExpression constant)
        {
            return new ConstantNode(parent, constant);
        }

        private static BinaryNode TreeNodeFor(ITreeNode parent, BinaryExpression binary)
        {
            return new BinaryNode(parent, binary);
        }

        private static ParameterNode TreeNodeFor(ITreeNode parent, ParameterExpression parameter)
        {
            return new ParameterNode(parent, parameter);
        }

        protected static LambdaNode TreeNodeFor(ITreeNode parent, LambdaExpression lambda)
        {
            return new LambdaNode(parent, lambda);
        }

        private static ITreeNode TreeNodeFor(ITreeNode parent, UnaryExpression unary)
        {
            return new UnaryNode(parent, unary);
            
//            var stripped = unary.StripQuotes();
//            var innerUnary = stripped as UnaryExpression;
//            if (innerUnary != null)
//            {
//                return new UnaryNode(parent, innerUnary);
//            }
//            return TreeNodeFor(parent, (dynamic)stripped);
        }

        private static MemberNode TreeNodeFor(ITreeNode parent, MemberExpression member)
        {
            return new MemberNode(parent, member);
        }

        private static ITreeNode TreeNodeFor(ITreeNode parent, MethodCallExpression method)
        {
            switch (method.Method.Name)
            {
                case("Select"):
                    return new SelectNode(parent, method);
                case("Where"):
                    return new WhereNode(parent, method);
                default:
                throw new InvalidOperationException(string.Format("Method not supported: {0}", method.Method.Name));
            }
        }

        private static ITreeNode TreeNodeFor(ITreeNode parent, object invalid)
        {
            throw new InvalidOperationException(string.Format("Don't know how to handle {0}", invalid.GetType()));
        }

        public override string ToString()
        {
            return Translate();
        }
    }
}