using System;
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

        public ClassBuilder WithAttribute<TAttribute>() where TAttribute : Attribute
        {
            var attribute = typeof(TAttribute).GetConstructor(new Type[0]);
            if (attribute == null)
            {
                throw new InvalidOperationException(string.Format("No parameterless constructor defined for {0}", typeof(TAttribute)));
            }
            var builder = new CustomAttributeBuilder(attribute, new object[0]);
            _typeBuilder.SetCustomAttribute(builder);
            return this;
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