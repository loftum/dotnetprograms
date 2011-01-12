namespace HourGlass.Lib.Domain
{
    public class HourUsage : DomainObject
    {
        public virtual Week Week { get; set; }
        public virtual HourCode HourCode { get; set; }

        public virtual double Monday { get; set; }
        public virtual double Tuesday { get; set; }
        public virtual double Wednesday { get; set; }
        public virtual double Thursday { get; set; }
        public virtual double Friday { get; set; }
        public virtual double Saturday { get; set; }
        public virtual double Sunday { get; set; }

        public virtual double Sum
        {
            get { return Monday + Tuesday + Wednesday + Thursday + Friday + Saturday + Sunday; }
        }

        public virtual void SetHourCode(HourCode hourCode)
        {
            if (HourCode != null)
            {
                HourCode.RemoveUsage(this);
            }
            HourCode = hourCode;
            if (hourCode != null)
            {
                hourCode.AddUsage(this);
            }
        }
    }
}