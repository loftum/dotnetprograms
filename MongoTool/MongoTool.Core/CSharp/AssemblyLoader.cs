using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MongoTool.Core.Configuration;

namespace MongoTool.Core.CSharp
{
    public class AssemblyLoader
    {
        public static AssemblySet PreviouslyLoadedAssemblies { get; private set; }
        public static AssemblySet NewAssemblies { get; private set; }
        public static string AssemblyFolder { get; private set; }

        static AssemblyLoader()
        {
            var type = typeof (Enumerable);
            PreviouslyLoadedAssemblies = new AssemblySet(AppDomain.CurrentDomain.GetAssemblies());
            NewAssemblies = new AssemblySet();
            AssemblyFolder = Path.Combine(Directory.GetCurrentDirectory(), "Assemblies");
            if (!Directory.Exists(AssemblyFolder))
            {
                Directory.CreateDirectory(AssemblyFolder);
            }
        }

        public void Load(AssemblyCollection collection)
        {
            try
            {
                if ( collection == null || !collection.Any())
                {
                    return;
                }
                AppDomain.CurrentDomain.AssemblyResolve += LoadFromAssemblyFolder;
                DoLoad(collection);
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= LoadFromAssemblyFolder;
            }
        }

        private static Assembly LoadFromAssemblyFolder(object sender, ResolveEventArgs args)
        {
            var filename = string.Format("{0}.dll", new AssemblyName(args.Name).Name);
            var paths = SearchPathsTo(filename).ToList();
            var path = paths.FirstOrDefault(File.Exists);
            if (path == null)
            {
                throw new ApplicationException(string.Format("Could not find assembly {0}", args.Name));
            }
            return Assembly.LoadFrom(path);
        }

        private static IEnumerable<string> SearchPathsTo(string filename)
        {
            yield return Path.Combine(Directory.GetCurrentDirectory(), filename);
            yield return Path.Combine(AssemblyFolder, filename);
        }

        private void DoLoad(AssemblyCollection collection)
        {
            CopyAllDllsFrom(collection.SourceFolder);
            
            var files = collection.Select(f => new FileInfo(Path.Combine(AssemblyFolder, f.FileName))).ToList();
            var nonexisting = files.Where(f => !f.Exists).ToArray();
            if (nonexisting.Any())
            {
                throw new InvalidOperationException(string.Format("Nonexisting dll's: {0}",
                    string.Join(", ", nonexisting.Select(f => f.FullName))));
            }
            foreach (var file in files)
            {
                Load(file.FullName);
            }
        }

        private static void CopyAllDllsFrom(string sourceFolder)
        {
            var destinationFolder = Path.Combine(Directory.GetCurrentDirectory(), "Assemblies");
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
            var sourceDir = new DirectoryInfo(sourceFolder);
            foreach (var file in sourceDir.EnumerateFiles("*.dll"))
            {
                var copyPath = Path.Combine(destinationFolder, file.Name);
                file.CopyTo(copyPath, true);
            }
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