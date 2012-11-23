using System;
using System.Reflection;
using System.Reflection.Emit;
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
            _assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);
            _module = _assembly.DefineDynamicModule(nameSpace);
        }

        public string Save()
        {
            var path = string.Format("{0}.dll", _nameSpace);
            _assembly.Save(path);
            return path;
        }

        public Type CreateType(TableMeta tableType)
        {
            var typeBuilder = _module.DefineType(tableType.TypeName, TypeAttributes.Public | TypeAttributes.Class);

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
            var generetedObject = Activator.CreateInstance(type);
            var properties = type.GetProperties();
            var propertiesCounter = 0;

            foreach (var column in record.Columns)
            {
                var value = record[column];
                properties[propertiesCounter].SetValue(generetedObject, value, null);
                propertiesCounter++;
            }
            return generetedObject;
        }
    }
}