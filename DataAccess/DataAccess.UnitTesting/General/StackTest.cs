using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DataAccess.UnitTesting.General
{
    [TestFixture]
    public class StackTest
    {
        [Test]
        public void Should()
        {
            var stack = new Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");
            
            foreach (var item in stack.Concat(new []{"x"}))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(stack.Count);
        }
    }
}