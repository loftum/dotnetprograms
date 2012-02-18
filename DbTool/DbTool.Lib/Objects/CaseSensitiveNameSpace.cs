using DbTool.Lib.Objects.TypeCaching;

namespace DbTool.Lib.Objects
{
    public class CaseSensitiveNameSpace : NameSpace<CaseSensitiveTypeCache>
    {
        public CaseSensitiveNameSpace(string name) : base(name)
        {
        }

        public override void Add(DbToolObject obj)
        {
            DoAdd(obj.Name, obj);
        }

        public override DbToolObject Get(string name)
        {
            return DoGet(name);
        }
    }
}