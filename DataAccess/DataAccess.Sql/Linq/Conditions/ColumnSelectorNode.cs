using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DataAccess.Sql.ExtensionMethods;

namespace DataAccess.Sql.Linq.Conditions
{
    public class ColumnSelectorNode : SqlNode
    {
        public IList<ColumnSelector> Selectors { get; private set; }

        public ColumnSelectorNode(NewExpression expression, SqlNode parent) : base(parent)
        {
//            if (expression.Arguments.Count != expression.Members.Count)
//            {
//                throw new InvalidOperationException(string.Format("Sorry, I can't figure out how to solve arguments ({0}) into ({1})",
//                    string.Join(", ", expression.Arguments.Select(a => a.Type)),
//                    string.Join(", ", expression.Members.Select(m => m.Name))));
//            }

            var otherProperties =
                expression.Type.GetProperties()
                    .Where(p => p.PropertyType.IsValueOrString() && ! expression.Members.Contains(p));
            var otherFields =
                expression.Type.GetFields()
                    .Where(f => f.FieldType.IsValueOrString() && ! expression.Members.Contains(f));

            Selectors = expression.Members
                .Select((m, ii) => new ColumnSelector(m, expression.Arguments[ii], this))
                .Concat(otherProperties.Select(m => new ColumnSelector(m, Expression.Constant(m.PropertyType.GetDefaultValue().ToDbValue()), this)))
                .Concat(otherFields.Select(m => new ColumnSelector(m, Expression.Constant(m.FieldType.GetDefaultValue().ToDbValue()), this)))
                .ToList();
        }

        public ColumnSelectorNode(MemberInitExpression expression, SqlNode parent) : base(parent)
        {
            var members = expression.Bindings.Select(b => b.Member).ToArray();
            var otherProperties =
                expression.Type.GetProperties()
                    .Where(p => p.PropertyType.IsValueOrString() && !members.Contains(p));
            var otherFields =
                expression.Type.GetFields()
                    .Where(f => f.FieldType.IsValueOrString() && !members.Contains(f));

            Selectors = expression.Bindings
                .Select(b => ColumnSelectorFor((dynamic) b))
                .Concat(otherProperties.Select(m => new ColumnSelector(m, Expression.Constant(m.PropertyType.GetDefaultValue().ToDbValue()), this)))
                .Concat(otherFields.Select(m => new ColumnSelector(m, Expression.Constant(m.FieldType.GetDefaultValue().ToDbValue()), this)))
                .Cast<ColumnSelector>()
                .ToList();
        }

        private ColumnSelector ColumnSelectorFor(MemberAssignment assignment)
        {
            return new ColumnSelector(assignment.Member, assignment.Expression, this);
        }

        private static ColumnSelector ColumnSelectorFor(object invalid)
        {
            throw new InvalidOperationException(string.Format("Don't know how to select column for {0}", invalid.GetType().GetFriendlyName()));
        }

        public override string ToSql()
        {
            return string.Join(", ", Selectors);
        }
    }
}