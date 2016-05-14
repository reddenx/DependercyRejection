using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main()
		{
			var devFolder = Environment.CurrentDirectory;
            var saveFileName = "DependercyRejection.derp";
            string inputAssembly;
			bool verbose = false;

            DependencyGraph dependencies = null;

            //load saved if exists
            if (File.Exists(saveFileName))
            {
                Console.WriteLine("Loading From Cache");
                dependencies = LoadFromFile(saveFileName);
            }

            //if save didn't populate
            if (dependencies == null)
            {
                dependencies = BuildFromDisk(devFolder);
                SaveToFile(saveFileName, dependencies);
            }



			//would be a good idea to save this....
			string consoleInput = string.Empty;

			do
			{
				Console.WriteLine("Enter Assembly name");
				consoleInput = Console.ReadLine();
				Console.Clear();
				inputAssembly = consoleInput.Split(' ')[0];
				verbose = consoleInput.Contains("-v");

                if (consoleInput.Contains("--refresh"))
                {
                    //refresh here
                    BuildFromDisk(devFolder);
                    continue;
                }

				Console.WriteLine("Finding Assembly {0}", inputAssembly);

                var selectedProjects = dependencies.ProjectFiles.Where(proj => proj.AssemblyName != null && proj.AssemblyName.ToLower() == inputAssembly.ToLower());
				if (!selectedProjects.Any())
				{
					Console.WriteLine("Assembly not found {0}", inputAssembly);
				}
				else
				{

					//based on input file, 
					var upTheGraph = GetDependantsForProject(selectedProjects.ToArray());
					var downTheGraph = GetProjectDependencies(selectedProjects.ToArray(), string.Empty, verbose);

					Console.WriteLine(
		@"
===================
Dependent Solutions
===================");
					int index = 1;
					foreach (var solution in upTheGraph.Item1.Distinct().OrderBy(sol => sol.FilePath))
					{
						Console.WriteLine("{0} - {1}", index++, solution.FilePath);
					}

					Console.WriteLine(@"
==================
Dependent Projects
==================");
					index = 1;
					foreach (var project in upTheGraph.Item2.Distinct().OrderBy(proj => proj.AssemblyName))
					{
						Console.WriteLine("{0} - {1}", index++, project.AssemblyName);
					}

					Console.WriteLine(@"
================
Project Requires
================");
					index = 1;
                    foreach (var project in downTheGraph.Distinct().OrderBy(proj => proj))
					{
						Console.WriteLine("{0} - {1}", index++, project);
					}

					Console.WriteLine("\r\nComplete...");
				}
			}
			while (consoleInput != "exit");
		}

        private static DependencyGraph BuildFromDisk(string devFolder)
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

        private static DependencyGraph LoadFromFile(string saveFileName)
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

        private static void SaveToFile(string saveFileName, DependencyGraph dependencies)
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

		private static Tuple<SolutionFile[], ProjectFile[]> GetDependantsForProject(ProjectFile[] inputProjects)
		{
			var solutions = new List<SolutionFile>(inputProjects.SelectMany(proj => proj.ReferencedBySolutions));
			var projects = new List<ProjectFile>(inputProjects.SelectMany(proj => proj.ReferencedByProjects));

			foreach (var dependentProject in inputProjects.SelectMany(proj => proj.ReferencedByProjects))
			{
				var dependants = GetDependantsForProject(new[] { dependentProject });
				solutions.AddRange(dependants.Item1);
				projects.AddRange(dependants.Item2);
			}

			return new Tuple<SolutionFile[], ProjectFile[]>(solutions.ToArray(), projects.ToArray());
		}

		private static string[] GetProjectDependencies(ProjectFile[] inputProjects, string treeBaseName, bool verbose)
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
