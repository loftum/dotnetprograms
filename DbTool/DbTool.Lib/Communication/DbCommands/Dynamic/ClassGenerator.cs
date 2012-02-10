using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using WebMatrix.Data;

namespace DbTool.Lib.Communication.DbCommands.Dynamic
{
    public class ClassGenerator
    {
        private const MethodAttributes GetSetAttr = MethodAttributes.Public | MethodAttributes.HideBySig;

        public static Type CreateType(string name, DynamicRecord record)
        {
            // create a dynamic assembly and module 
            var assemblyName = new AssemblyName("tmpAssembly");

            var assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var module = assemblyBuilder.DefineDynamicModule("tmpModule");

            // create a new type builder
            var typeBuilder = module.DefineType(name, TypeAttributes.Public | TypeAttributes.Class);

            foreach (var column in record.Columns)
            {
                var propertyName = column;
                var propertyType = record[column].GetType();
                var field = typeBuilder.DefineField("_" + propertyName, typeof(string), FieldAttributes.Private);
                var property = typeBuilder.DefineProperty(propertyName,
                                     PropertyAttributes.None,
                                     typeof(string),
                                     new[] { propertyType });

                var currGetPropMthdBldr = typeBuilder.DefineMethod("get_value",
                                               GetSetAttr,
                                               propertyType,
                                               Type.EmptyTypes);

                // Intermediate Language stuff...
                var currGetIl = currGetPropMthdBldr.GetILGenerator();
                currGetIl.Emit(OpCodes.Ldarg_0);
                currGetIl.Emit(OpCodes.Ldfld, field);
                currGetIl.Emit(OpCodes.Ret);

                // Define the "set" accessor method for current private field.
                MethodBuilder currSetPropMthdBldr =
                    typeBuilder.DefineMethod("set_value",
                                               GetSetAttr,
                                               null,
                                               new[] { propertyType });

                // Again some Intermediate Language stuff...
                var currSetIL = currSetPropMthdBldr.GetILGenerator();
                currSetIL.Emit(OpCodes.Ldarg_0);
                currSetIL.Emit(OpCodes.Ldarg_1);
                currSetIL.Emit(OpCodes.Stfld, field);
                currSetIL.Emit(OpCodes.Ret);

                // Last, we must map the two methods created above to our PropertyBuilder to 
                // their corresponding behaviors, "get" and "set" respectively. 
                property.SetGetMethod(currGetPropMthdBldr);
                property.SetSetMethod(currSetPropMthdBldr);
            }

            return typeBuilder.CreateType();
        }

        public static object Cast(DynamicRecord record, Type type)
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