using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using DbTool.Lib.Communication.DbCommands.Dynamic;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta
{
    public class TableTypeGenerator
    {
        private const MethodAttributes GetSetAttr = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;
        private readonly ModuleBuilder _module;
        private readonly AssemblyBuilder _assembly;
        private readonly string _nameSpace;

        public TableTypeGenerator(string nameSpace)
        {
            _nameSpace = nameSpace;
            var assemblyName = new AssemblyName(nameSpace);
            _assembly = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);
            _module = _assembly.DefineDynamicModule(nameSpace);
        }

        public void Save()
        {
            _assembly.Save(string.Format("{0}.dll", _nameSpace));
        }

        public Type CreateType(TableMeta tableType)
        {
            var typeBuilder = _module.DefineType(string.Format("{0}.{1}", _nameSpace, tableType.TypeName), TypeAttributes.Public | TypeAttributes.Class);

            foreach (var column in tableType.Columns)
            {
                var field = typeBuilder.DefineField(string.Format("_{0}", column.MemberName), column.CSharpType, FieldAttributes.Private);
                var property = typeBuilder.DefineProperty(column.MemberName,
                                     PropertyAttributes.None,
                                     column.CSharpType,
                                     new[] { column.CSharpType });

                var getter = typeBuilder.DefineMethod(string.Format("get_{0}", column.MemberName),
                                               GetSetAttr,
                                               column.CSharpType,
                                               Type.EmptyTypes);

                var il = getter.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, field);
                il.Emit(OpCodes.Ret);

                var setter = typeBuilder.DefineMethod(string.Format("set_{0}", column.MemberName),
                                               GetSetAttr,
                                               null,
                                               new[] { column.CSharpType });

                var currSetIL = setter.GetILGenerator();
                currSetIL.Emit(OpCodes.Ldarg_0);
                currSetIL.Emit(OpCodes.Ldarg_1);
                currSetIL.Emit(OpCodes.Stfld, field);
                currSetIL.Emit(OpCodes.Ret);

                property.SetGetMethod(getter);
                property.SetSetMethod(setter);
            }

            return typeBuilder.CreateType();
        }

        public static object Cast(DynamicDataRow record, Type type)
        {
            // Now we have our type. Let's create an instance from it:
            var generetedObject = Activator.CreateInstance(type);

            // Loop over all the generated properties, and assign the values from our XML:
            var properties = type.GetProperties();

            var propertiesCounter = 0;

            // Loop over the values that we will assign to the properties
            foreach (var column in record.Columns)
            {
                var value = record[column];
                properties[propertiesCounter].SetValue(generetedObject, value, null);
                propertiesCounter++;
            }

            //Yoopy ! Return our new genereted object.
            return generetedObject;
        }
    }
}