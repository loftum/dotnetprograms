﻿using System;
using System.Reflection;

namespace DotNetPrograms.Common.Meta
{
    public class MethodMeta : MemberMeta
    {
        private readonly MethodInfo _method;

        public override string Name { get { return _method.Name; } }

        public MethodMeta(MethodInfo method)
        {
            _method = method;
        }

        public bool IsProxiable
        {
            get
            {
                return !_method.IsPrivate && !_method.IsFinal && (_method.IsVirtual || _method.IsAbstract);
            }
        }

        public bool HasCustomAttribute<T>() where T : Attribute
        {
            return GetCustomAttribute<T>() != null;
        }

        public T GetCustomAttribute<T>() where T : Attribute
        {
            return _method.GetCustomAttribute<T>();
        }
    }
}