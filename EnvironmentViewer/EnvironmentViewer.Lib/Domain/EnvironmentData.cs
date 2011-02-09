using EnvironmentViewer.Lib.Extensions;

namespace EnvironmentViewer.Lib.Domain
{
    public class EnvironmentData : DomainObject
    {
        public virtual string Name { get; set; }
        public virtual string Host { get; set; }
        public virtual string Url { get; set; }
        public virtual string DatabaseType { get; set; }
        public virtual string DatabaseHost { get; set; }
        public virtual string DatabaseName { get; set; }
        public virtual string DatabaseUsername { get; set; }
        public virtual string DatabasePassword { get; set; }
        public virtual bool IntegratedSecurity { get; set; }
        public virtual bool HasValidUrl
        {
            get { return !Url.IsNullOrWhiteSpace() && Url.StartsWith("http://"); }
        }
    }
}