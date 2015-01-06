using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MongoTool.Core.CSharp
{
    public class AssemblyLoader
    {
        public static AssemblySet PreviouslyLoadedAssemblies { get; private set; }
        public static AssemblySet NewAssemblies { get; private set; }
        
        static AssemblyLoader()
        {
            PreviouslyLoadedAssemblies = new AssemblySet(AppDomain.CurrentDomain.GetAssemblies());
            NewAssemblies = new AssemblySet();
        }

        public void Load(IEnumerable<string> paths)
        {
            var files = paths.Select(p => new FileInfo(p)).ToList();
            var nonexisting = files.Where(f => !f.Exists).ToArray();
            if (nonexisting.Any())
            {
                throw new InvalidOperationException(string.Format("Nonexisting dll's: {0}", string.Join(", ", nonexisting.Select(f => f.FullName))));
            }

            var copies = files.Select(CopyToCurrent).ToList();

            foreach (var copy in copies)
            {
                Load(copy);
            }
        }

        private static string CopyToCurrent(FileInfo file)
        {
            var copyPath = Path.Combine(Directory.GetCurrentDirectory(), file.Name);
            File.Copy(file.FullName, copyPath, true);
            return copyPath;
        }
        
        private static bool ShouldLoad(AssemblyName assemblyName)
        {
            var shouldLoad = !NewAssemblies.Contains(assemblyName) && !PreviouslyLoadedAssemblies.Contains(assemblyName);
            return shouldLoad;
        }

        private void Load(string path)
        {
            var assembly = Assembly.LoadFile(path);
            if (ShouldLoad(assembly.GetName()))
            {
                NewAssemblies.Add(assembly);
                LoadReferenced(assembly);
            }
        }

        private void Load(AssemblyName assemblyName)
        {
            if (ShouldLoad(assemblyName))
            {
                var assembly = Assembly.Load(assemblyName);
                NewAssemblies.Add(assembly);
                LoadReferenced(assembly);
            }
        }

        private void LoadReferenced(Assembly assembly)
        {
            var referencedAssemblies = assembly.GetReferencedAssemblies().Where(ShouldLoad).ToList();
            foreach (var referencedAssembly in referencedAssemblies)
            {
                Load(referencedAssembly);
            }
        }
    }
}