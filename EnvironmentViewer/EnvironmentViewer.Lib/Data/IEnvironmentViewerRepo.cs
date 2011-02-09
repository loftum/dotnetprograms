using System.Collections.Generic;
using EnvironmentViewer.Lib.Domain;

namespace EnvironmentViewer.Lib.Data
{
    public interface IEnvironmentViewerRepo
    {
        IEnumerable<EnvironmentData> GetAll();
        void SaveAll(IEnumerable<EnvironmentData> environments);
    }
}