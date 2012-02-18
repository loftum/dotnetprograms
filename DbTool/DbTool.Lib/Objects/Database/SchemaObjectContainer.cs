using DbTool.Lib.Objects.TypeCaching;

namespace DbTool.Lib.Objects.Database
{
    public class SchemaObjectContainer : ObjectContainerBase<DatabaseNameSpace, CaseInsensitiveTypeCache>
    {
        public override DatabaseNameSpace CreateNameSpace(string name)
        {
            return new DatabaseNameSpace(name);
        }
    }
}