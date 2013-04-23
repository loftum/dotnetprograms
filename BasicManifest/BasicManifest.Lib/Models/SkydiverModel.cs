namespace BasicManifest.Lib.Models
{
    public class SkydiverModel
    {
        public long Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FullName { get { return string.Join(" ", FirstName, LastName).Trim(); } }
    }
}