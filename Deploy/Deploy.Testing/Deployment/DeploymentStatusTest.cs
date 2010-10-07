using Deploy.Lib.Deployment;
using NUnit.Framework;

namespace Deploy.Testing.Deployment
{
    [TestFixture]
    public class DeploymentStatusTest
    {
        [Test]
        public void ShouldCalculateSuccessfulWhenAllStepsAreSuccessful()
        {
            var deployStatus = new DeploymentStatus();
            deployStatus.Add(new DeploymentStepStatus{Status = DeploymentStepStatus.Ok});
            Assert.That(deployStatus.Status, Is.EqualTo("Successful"));
        }

        [Test]
        public void ShouldCalculateFailedWhenOneStepIsFailed()
        {
            var deployStatus = new DeploymentStatus();
            deployStatus.Add(new DeploymentStepStatus { Status = DeploymentStepStatus.Ok });
            deployStatus.Add(new DeploymentStepStatus { Status = DeploymentStepStatus.Warning });
            deployStatus.Add(new DeploymentStepStatus { Status = DeploymentStepStatus.Fail });
            Assert.That(deployStatus.Status, Is.EqualTo("Failed"));
        }

        [Test]
        public void ShouldCalculateWarningWhenOneStepHasWarning()
        {
            var deployStatus = new DeploymentStatus();
            deployStatus.Add(new DeploymentStepStatus { Status = DeploymentStepStatus.Ok });
            deployStatus.Add(new DeploymentStepStatus { Status = DeploymentStepStatus.Warning });
            Assert.That(deployStatus.Status, Is.EqualTo("Warning"));
        }
    }
}
