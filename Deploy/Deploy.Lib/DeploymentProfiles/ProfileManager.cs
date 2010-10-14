using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Deploy.Lib.Configuration;

namespace Deploy.Lib.DeploymentProfiles
{
    public class ProfileManager : IProfileManager
    {
        private static ProfileManager _instance;
        private readonly IList<DeploymentProfile> _profiles = new List<DeploymentProfile>();

        public static ProfileManager Instance
        {
            get { return _instance ?? (_instance = new ProfileManager()); }
        }

        private ProfileManager()
        {
            ReadProfiles();
        }

        private void ReadProfiles()
        {
            var profileDirectory = new DirectoryInfo(DeploymentConfiguration.ProfileFolder);
            if (!profileDirectory.Exists)
            {
                profileDirectory.Create();
                return;
            }
            foreach(var file in profileDirectory.GetFiles("*.xml"))
            {
                var serializer = new XmlSerializer(typeof (DeploymentProfile));
                try
                {
                    using (var stream = file.OpenRead())
                    {
                        var deploymentProfile = (DeploymentProfile)serializer.Deserialize(stream);
                        _profiles.Add(deploymentProfile);
                    }
                }
                catch (InvalidOperationException)
                {
                    
                }
            }
        }

        public IEnumerable<DeploymentProfile> GetAll()
        {
            return _profiles;
        }

        public void Add(DeploymentProfile deploymentProfile)
        {
            VerifyNoProfileWithSameName(deploymentProfile);
            _profiles.Add(deploymentProfile);
        }

        public void Save(DeploymentProfile deploymentProfile)
        {
            VerifyNoProfileWithSameName(deploymentProfile);
            var profileDirectory = new DirectoryInfo(DeploymentConfiguration.ProfileFolder);
            if (!profileDirectory.Exists)
            {
                profileDirectory.Create();
            }
            var profileFilename = Path.Combine(profileDirectory.FullName, deploymentProfile.Name + ".xml");
            var serializer = new XmlSerializer(typeof(DeploymentProfile));
            try
            {
                using (var stream = File.Create(profileFilename, 4096, FileOptions.None))
                {
                    serializer.Serialize(stream, deploymentProfile);
                }
            }
            catch (InvalidOperationException e)
            {
                throw new DeploymentProfileException("Could not save " + deploymentProfile.Name, e);
            }
        }

        public void VerifyNoProfileWithSameName(DeploymentProfile deploymentProfile)
        {
            if (_profiles.Any(profile => profile.Name.Equals(deploymentProfile.Name)))
            {
                throw new DeploymentProfileException("Profile " + deploymentProfile.Name + " already exists");
            }
        }
    }
}
