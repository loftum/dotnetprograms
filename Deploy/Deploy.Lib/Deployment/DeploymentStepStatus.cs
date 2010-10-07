using System;
using System.Text;

namespace Deploy.Lib.Deployment
{
    [Serializable]
    public class DeploymentStepStatus
    {
        public const int Ok = 0;
        public const int Warning = 1;
        public const int Fail = 2;

        public bool CanProceed { get; set; }
        public int Status { get; set; }
        public string Exception { get; set; }
        public string Comment { get; set; }

        public DeploymentStepStatus()
        {
            CanProceed = true;
        }

        public DeploymentStepStatus(bool canProceed, int status, string comment = null, string exception = null)
        {
            CanProceed = canProceed;
            Status = status;
            Comment = string.IsNullOrEmpty(comment) ? string.Empty : comment;
            Exception = string.IsNullOrEmpty(exception) ? string.Empty : exception;
        }

        public DeploymentStepStatus AppendComment(string comment)
        {
            Comment = new StringBuilder(Comment).Append(comment).ToString();
            return this;
        }

        public DeploymentStepStatus AppendCommentLine(string comment)
        {
            Comment = new StringBuilder(Comment).AppendLine(comment).ToString();
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
                default:
                    return "Ok";
            }
        }
    }
}
