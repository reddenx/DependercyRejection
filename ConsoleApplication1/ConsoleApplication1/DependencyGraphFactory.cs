using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class DependencyGraphFactory
    {
        public static DependencyGraph BuildFromDisk(string devFolder)
        {
            Console.WriteLine("Gathering Projects From: {0}", devFolder);
            var projectsFilePaths = Directory.GetFiles(devFolder, "*.csproj", SearchOption.AllDirectories);
            Console.WriteLine("Found: {0}", projectsFilePaths.Length);

            Console.WriteLine("Gathering Solutions From: {0}", devFolder);
            var solutionFilePaths = Directory.GetFiles(devFolder, "*.sln", SearchOption.AllDirectories);
            Console.WriteLine("Found: {0}", solutionFilePaths.Length);

            Console.WriteLine("Constructing Data", projectsFilePaths.Count());
            var projects = projectsFilePaths.Select(projectPath => ProjectFile.BuildFromFile(projectPath)).ToArray();
            var solutions = solutionFilePaths.Select(solutionPath => SolutionFile.BuildFromFile(solutionPath, projects)).ToArray();

            Console.WriteLine("Building Dependency Graph, {0} operations expected", projects.Length * projects.Length * solutions.Length);
            //build dependency graph, could be done in linq but meh
            foreach (var outerProject in projects)
            {
                foreach (var innerProject in projects)
                {
                    if (outerProject.ReferencesProjectIds.Contains(innerProject.ProjectId))
                    {
                        outerProject.ReferencesProjects.Add(innerProject);
                        innerProject.ReferencedByProjects.Add(outerProject);
                    }
                }
            }
            return new DependencyGraph(projects, solutions);
        }

        public static DependencyGraph LoadFromFile(string saveFileName)
        {
            var formatter = new BinaryFormatter();
            try
            {
                using (var inStream = File.OpenRead(saveFileName))
                {
                    return (DependencyGraph)formatter.Deserialize(inStream);
                }
            }
            catch
            {
                return null;
            }
        }

        public static void SaveToFile(string saveFileName, DependencyGraph dependencies)
        {
            Console.WriteLine("Complete, saving to: {0}\r\n --refresh to refresh from disk", saveFileName);
            try
            {
                try { File.Delete(saveFileName); }
                catch { }

                using (var stream = File.OpenWrite(saveFileName))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, dependencies);
                }

            }
            catch
            {
                Console.WriteLine("Unable to save file: {0}", saveFileName);
            }
        }

        public static DependencyGraph GetDependantsForProject(ProjectFile[] inputProjects)
        {
            var solutions = new List<SolutionFile>(inputProjects.SelectMany(proj => proj.ReferencedBySolutions));
            var projects = new List<ProjectFile>(inputProjects.SelectMany(proj => proj.ReferencedByProjects));

            foreach (var dependentProject in inputProjects.SelectMany(proj => proj.ReferencedByProjects))
            {
                var dependants = GetDependantsForProject(new[] { dependentProject });
                solutions.AddRange(dependants.SolutionFiles);
                projects.AddRange(dependants.ProjectFiles);
            }

            return new DependencyGraph(projects.ToArray(), solutions.ToArray());
        }

		public static string[] GetProjectDependencies(ProjectFile[] inputProjects, string treeBaseName, bool verbose)
        {
            var projects = new List<ProjectFile>(inputProjects.SelectMany(proj => proj.ReferencesProjects));
            var dependencies = new List<string>();

            foreach (var reference in inputProjects.SelectMany(proj => proj.ReferencesProjects))
            {
                var treeBase = reference.AssemblyName;
                if (verbose)
                {
                    treeBase = treeBaseName + "->" + reference.AssemblyName;
                }
                var references = GetProjectDependencies(new[] { reference }, treeBase, verbose);
                dependencies.Add(treeBase);
                dependencies.AddRange(references);
            }

            return dependencies.ToArray();
        }

    }
}
