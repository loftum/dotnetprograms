using DbTool.Lib.Objects.TypeCaching;

namespace DbTool.Lib.Objects.CSharp
{
    public class CSharpObjectContainer : ObjectContainerBase<CSharpNameSpace, CaseSensitiveTypeCache>
    {
        public override CSharpNameSpace CreateNameSpace(string name)
        {
            return new CSharpNameSpace(name);
        }
    }
}