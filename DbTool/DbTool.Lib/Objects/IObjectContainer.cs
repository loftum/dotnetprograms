using System;
using System.Collections.Generic;

namespace DbTool.Lib.Objects
{
    public interface IObjectContainer
    {
        IEnumerable<DbToolObject> GetAllObjects();
        DbToolObject GetObject(string name);
        Type GetObjectType(string name);
    }
}