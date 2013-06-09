using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace DbTool.Lib.Meta.Emit
{
    public class DynamicAssembly
    {
        private readonly ModuleBuilder _module;
        private readonly string _nameSpace;
        private readonly string _fileName;
        private readonly AssemblyBuilder _assembly;

        public DynamicAssembly(string nameSpace)
        {
            _nameSpace = nameSpace;
            _fileName = string.Format("{0}.dll", _nameSpace);
            var assemblyName = new AssemblyName(nameSpace);
            _assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            _module = _assembly.DefineDynamicModule(nameSpace, _fileName);
        }

        public AssemblyName Name
        {
            get { return Assembly.GetName(); }
        }

        public string FullName
        {
            get { return Assembly.FullName; }
        }

        public Assembly Assembly { get { return _assembly; } }

        public ClassBuilder BuildClass(string name)
        {
            var typeName = string.Format("{0}.{1}", _nameSpace, name);
            var typeBuilder = _module.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class);
            return new ClassBuilder(typeBuilder);
        }

        public byte[] GetBytes()
        {
            _assembly.Save(_fileName);
            return File.ReadAllBytes(_fileName);
        }

        public void Save()
        {
            _assembly.Save(string.Format("{0}.dll", _nameSpace));
        }

        public IEnumerable<Type> GetTypes()
        {
            return _assembly.GetTypes();
        }
    }
}