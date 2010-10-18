using System;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.Lib.Deployment
{
    [Serializable]
    public class DeploymentStepStatus
    {
        public const int Ok = 0;
        public const int Warning = 1;
        public const int Fail = 2;
        public const int Skipped = 4;

        public string StepName { get; set; }
        public int Status { get; set; }
        public string Details { get; set; }
        public string Error { get; set; }
        [XmlIgnore]
        public bool CanProceed { get; set; }

        public DeploymentStepStatus()
        {
            CanProceed = true;
        }

        public DeploymentStepStatus AppendDetails(string details)
        {
            Details = new StringBuilder(Details).Append(details).ToString();
            return this;
        }

        public DeploymentStepStatus AppendDetailsLine(string details)
        {
            Details = new StringBuilder(Details).AppendLine(details).ToString();
            return this;
        }

        public override string ToString()
        {
            switch(Status)
            {
                case Ok:
                    return "Done";
                case Warning:
                    return "Warning";
                case Fail:
                    return "Failed";
                case Skipped:
                    return "Skipped";
                default:
                    return "Ok";
            }
        }
    }
}
