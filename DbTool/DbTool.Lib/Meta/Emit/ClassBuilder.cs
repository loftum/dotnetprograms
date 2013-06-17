using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace DbTool.Lib.Meta.Emit
{
    public class ClassBuilder
    {
        private const MethodAttributes GetSetAttr = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;

        private readonly TypeBuilder _typeBuilder;

        public ClassBuilder(TypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;
        }

        public ClassBuilder WithAttribute<TAttribute>(params object[] ctorArguments) where TAttribute : Attribute
        {
            var attribute = typeof(TAttribute).GetConstructor(ctorArguments.Select(a => a.GetType()).ToArray());
            if (attribute == null)
            {
                var arguments = ctorArguments.Select((ii, a) => string.Format("{0} arg{1}", a.GetType().Name, ii));
                throw new InvalidOperationException(string.Format("There is no ctor {0}({1})", typeof(TAttribute), string.Join(", ", arguments)));
            }
            var builder = new CustomAttributeBuilder(attribute, ctorArguments);
            _typeBuilder.SetCustomAttribute(builder);
            return this;
        }

        public ClassBuilder WithAttribute<TAttribute>(Expression<Func<TAttribute>> expression)
            where TAttribute : Attribute
        {
            return WithAttribute<TAttribute>(((NewExpression) expression.Body).Arguments.Select(GetValue));
        }

        private static object GetValue(Expression expression)
        {
            return DoGetValue((dynamic) expression);
        }

        private static object DoGetValue(MemberExpression expression)
        {
            throw new NotImplementedException();
        }

        private static object DoGetValue(object invalid)
        {
            throw new InvalidOperationException(string.Format("Could not get value from {0}", invalid.GetType()));
        }

        public void AddProperty(string name, Type type)
        {
            var field = _typeBuilder.DefineField(string.Format("_{0}", name), type, FieldAttributes.Private);
            var property = _typeBuilder.DefineProperty(name,
                                 PropertyAttributes.None,
                                 type,
                                 new[] { type });

            var getter = _typeBuilder.DefineMethod(string.Format("get_{0}", name),
                                           GetSetAttr,
                                           type,
                                           Type.EmptyTypes);

            var getterIL = getter.GetILGenerator();
            getterIL.Emit(OpCodes.Ldarg_0);
            getterIL.Emit(OpCodes.Ldfld, field);
            getterIL.Emit(OpCodes.Ret);

            var setter = _typeBuilder.DefineMethod(string.Format("set_{0}", name),
                                           GetSetAttr,
                                           null,
                                           new[] { type });

            var setterIL = setter.GetILGenerator();
            setterIL.Emit(OpCodes.Ldarg_0);
            setterIL.Emit(OpCodes.Ldarg_1);
            setterIL.Emit(OpCodes.Stfld, field);
            setterIL.Emit(OpCodes.Ret);

            property.SetGetMethod(getter);
            property.SetSetMethod(setter);
        }

        public Type CreateType()
        {
            return _typeBuilder.CreateType();
        }
    }
}