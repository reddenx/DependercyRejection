using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using AltSource.Utilities.VSSolution.Filters;

namespace AltSource.Utilities.VSSolution
{
    public class DependencyGraphFactory
    {
        public event EventHandler<string> OutputLog;
        private void SafeLogRunner(string entry) { if (OutputLog != null) OutputLog(this, entry); }

        public DependencyGraph BuildFromDisk(string devFolder)
        {
            try
            {
                SafeLogRunner(string.Format("Gathering Projects From: {0}", devFolder));
                var projectsFilePaths = Directory.GetFiles(devFolder, "*.csproj", SearchOption.AllDirectories);
                SafeLogRunner(string.Format("Found: {0}", projectsFilePaths.Length));

                SafeLogRunner(string.Format("Gathering Solutions From: {0}", devFolder));
                var solutionFilePaths = Directory.GetFiles(devFolder, "*.sln", SearchOption.AllDirectories);
                SafeLogRunner(string.Format("Found: {0}", solutionFilePaths.Length));

                SafeLogRunner(string.Format("Constructing Data", projectsFilePaths.Count()));
                var projects = projectsFilePaths.Select(projectPath => ProjectFile.Build(projectPath)).ToArray();
                var solutions = solutionFilePaths.Select(solutionPath => SolutionFile.BuildFromFile(solutionPath, projects)).ToArray();

                SafeLogRunner(string.Format("Building Dependency Graph, {0} operations expected", projects.Length * projects.Length * solutions.Length));

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

                SafeLogRunner("Complete!");
                return new DependencyGraph(projects, solutions);
            }
            catch(IOException)
            {
                SafeLogRunner("Unable to gather projects from that directory");
                return null;
            }
        }

        public DependencyGraph LoadFromFile(string saveFileName)
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

        public void SaveToFile(string saveFileName, DependencyGraph dependencies)
        {
            SafeLogRunner(string.Format("Complete, saving to: {0}\r\n --refresh to refresh from disk", saveFileName));
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
                SafeLogRunner(string.Format("Unable to save file: {0}", saveFileName));
            }
        }

        public DependencyGraph GetDependantsForProject(ProjectFile[] inputProjects)
        {
            return new DependencyGraph(inputProjects.SelectMany(proj => proj.GetAncestors()).ToArray(), inputProjects.SelectMany(p => p.WhosMyDaddys()).ToArray());
        }

        public string[] GetProjectDependencies(ProjectFile[] inputProjects, string treeBaseName, bool verbose)
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
