using System;
using DbTool.Lib.Objects.CSharp;
using DbTool.Lib.Objects.Database;

namespace DbTool.Lib.Objects
{
    public interface IObjectCache
    {
        SchemaObjectContainer Schema { get; set; }
        CSharpObjectContainer CSharpObjects { get; set; }
        DbToolObject GetObject(string name);
        Type GetObjectType(string name);
    }
}