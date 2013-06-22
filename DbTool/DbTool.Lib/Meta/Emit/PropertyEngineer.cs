using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta.Emit
{
    public class PropertyEngineer
    {
        private const MethodAttributes GetSetAttr = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;

        private readonly string _name;
        private readonly Type _type;
        private readonly TypeBuilder _typeBuilder;
        private readonly PropertyBuilder _propertyBuilder;
        private readonly FieldBuilder _fieldBuilder;

        public PropertyEngineer(string name, Type type, TypeBuilder typeBuilder)
        {
            _name = name;
            _type = type;
            _typeBuilder = typeBuilder;
            
            _fieldBuilder = _typeBuilder.DefineField(string.Format("_{0}", _name), _type, FieldAttributes.Private);
            _propertyBuilder = _typeBuilder.DefineProperty(_name,
                                 PropertyAttributes.None,
                                 _type,
                                 new[] { _type });
        }

        public PropertyEngineer WithGetter()
        {
            var getter = _typeBuilder.DefineMethod(string.Format("get_{0}", _name),
                                               GetSetAttr,
                                               _type,
                                               Type.EmptyTypes);

            var getterIL = getter.GetILGenerator();
            getterIL.Emit(OpCodes.Ldarg_0);
            getterIL.Emit(OpCodes.Ldfld, _fieldBuilder);
            getterIL.Emit(OpCodes.Ret);
            _propertyBuilder.SetGetMethod(getter);
            return this;
        }

        public PropertyEngineer WithSetter()
        {
            var setter = _typeBuilder.DefineMethod(string.Format("set_{0}", _name),
                                               GetSetAttr,
                                               null,
                                               new[] { _type });

            var setterIL = setter.GetILGenerator();
            setterIL.Emit(OpCodes.Ldarg_0);
            setterIL.Emit(OpCodes.Ldarg_1);
            setterIL.Emit(OpCodes.Stfld, _fieldBuilder);
            setterIL.Emit(OpCodes.Ret);
            _propertyBuilder.SetSetMethod(setter);    
            return this;
        }

        public PropertyEngineer WithAttribute<TAttribute>(params object[] ctorArguments) where TAttribute : Attribute
        {
            return WithAttribute<TAttribute>(ctorArguments.ToList());
        }

        public PropertyEngineer WithAttribute<TAttribute>(Expression<Func<TAttribute>> expression)
            where TAttribute : Attribute
        {
            var arguments = (((NewExpression)expression.Body)).Arguments;
            return WithAttribute<TAttribute>(arguments.Select(GetValue).ToList());
        }

        public PropertyEngineer WithAttribute<TAttribute>(IEnumerable<object> ctorArguments)
        {
            var attribute = typeof(TAttribute).GetConstructor(ctorArguments.Select(a => a.GetType()).ToArray());
            if (attribute == null)
            {
                var arguments = ctorArguments.Select((a, ii) => string.Format("{0} arg{1}", a.GetType().Name, ii));
                throw new InvalidOperationException(string.Format("There is no ctor {0}({1})", typeof(TAttribute), string.Join(", ", arguments)));
            }
            var builder = new CustomAttributeBuilder(attribute, ctorArguments.ToArray());
            _propertyBuilder.SetCustomAttribute(builder);
            return this;
        }

        private static object GetValue(Expression expression)
        {
            return DoGetValue((dynamic)expression);
        }

        private static object DoGetValue(Expression expression)
        {
            var member = Expression.Convert(expression, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(member);
            var value = lambda.Compile()();
            return value;
        }

        private static object DoGetValue(object invalid)
        {
            throw new InvalidOperationException(string.Format("Could not get value from {0}", invalid.GetType()));
        }
    }
}