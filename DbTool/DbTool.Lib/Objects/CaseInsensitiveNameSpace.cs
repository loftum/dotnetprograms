using DbTool.Lib.Objects.TypeCaching;

namespace DbTool.Lib.Objects
{
    public class CaseInsensitiveNameSpace : NameSpace<CaseInsensitiveTypeCache>
    {
        public CaseInsensitiveNameSpace(string name) : base(name)
        {
        }

        public override DbToolObject Get(string name)
        {
            return DoGet(name.ToLowerInvariant());
        }

        public override void Add(DbToolObject obj)
        {
            DoAdd(obj.Name.ToLowerInvariant(), obj);
        }
    }
}