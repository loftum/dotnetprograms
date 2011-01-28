namespace EnvironmentViewer.Lib.Domain
{
    public class Environment : DomainObject
    {
        public virtual string Name { get; set; }
        public virtual string Host { get; set; }
        public virtual string DatabaseType { get; set; }
        public virtual string DatabaseHost { get; set; }
        public virtual string DatabaseName { get; set; }
        public virtual string DatabaseUsername { get; set; }
        public virtual string DatabasePassword { get; set; }
        public virtual bool IntegratedSecurity { get; set; }
    }
}