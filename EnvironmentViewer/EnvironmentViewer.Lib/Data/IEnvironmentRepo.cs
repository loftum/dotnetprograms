using System.Collections.Generic;
using EnvironmentViewer.Lib.Domain;

namespace EnvironmentViewer.Lib.Data
{
    public interface IEnvironmentRepo
    {
        IEnumerable<Environment> GetAll();
        void SaveAll(IEnumerable<Environment> environments);
    }
}