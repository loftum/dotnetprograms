using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMoq;
using DotNetPrograms.Common.ExtensionMethods;
using MissingLinq.Sql;
using NUnit.Framework;

namespace MissingLinq.UnitTesting
{
    [TestFixture]
    public class MissingLinqQueryableTest
    {
        private static bool _recursive = true;

        [Test]
        public void Should()
        {
            var expression = Expression.Constant(new Hest());
            Console.WriteLine(expression.Value);
        }

        [Test]
        public void Should2()
        {
            var mocker = new AutoMoqer();
            var provider = mocker.Create<MissingLinqQueryProvider>();
            var expression = new MissingLinqQueryable<Hest>(provider)
                .Where(p => p.Name == "Per")
                //                .OrderBy(h => h.Age)
                //                .Select(h => h)
                .Expression;
            Describe((dynamic)expression);
        }

        private static void Describe(LambdaExpression expression)
        {
            Console.WriteLine("Lambda!");
            Describe((Expression)expression);
        }

        private static void Describe(Expression expression)
        {
            Console.WriteLine("Type: {0}", expression.GetType().Name);
            Print(() => expression);
        }

        private static void Describe(ConstantExpression expression)
        {
            Describe((Expression)expression);
            Print(() => expression.Value);
        }

        private static void Describe(UnaryExpression expression)
        {
            Describe((Expression)expression);
            Describe((dynamic)expression.Operand);
        }

        private static void Describe(MethodCallExpression expression)
        {
            Describe((Expression)expression);
            Print(() => expression.Method.Name);
            Print(() => expression.Type.GetGenericArguments()[0]);

            if (!_recursive)
            {
                return;
            }
            _recursive = false;
            Console.WriteLine("Arguments:");
            foreach (var argument in expression.Arguments)
            {
                Describe((dynamic)argument);
            }
        }

        private static void Describe(object invalid)
        {
            Console.WriteLine(invalid.GetType().Name);
        }

        private static void Print<T>(Expression<Func<T>> expression)
        {
            Console.WriteLine("{0}: {1}", expression.GetPropertyName(), expression.Compile().Invoke());
        }
    }
}