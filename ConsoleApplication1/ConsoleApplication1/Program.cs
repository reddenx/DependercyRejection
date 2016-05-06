using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			var devFolder = Environment.CurrentDirectory;

			if (!File.Exists(args[0]))
			{
				Console.WriteLine("Please drop or supply a file");
				return;
			}

			var selectedProject = BuildProjectFromFile(args[0]);
			var projectsFilePaths = Directory.GetFiles(devFolder, "*.csproj", SearchOption.AllDirectories);
			var solutionFilePaths = Directory.GetFiles(devFolder, "*.sln", SearchOption.AllDirectories);

			//get all projects from parent folder
			//build dependency graph
			//get all solutions from parent folder
			//assign projects to solutions

			Console.WriteLine("Gathering Projects ({0})", projectsFilePaths.Count());
			var projects = projectsFilePaths.Select(projectPath => BuildProjectFromFile(projectPath)).ToArray();

			Console.WriteLine("Gathering Solutions {0})", solutionFilePaths.Count());
			var solutions = solutionFilePaths.Select(solutionPath => BuildSolutionFromFile(solutionPath, projects)).ToArray();

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

			//would be a good idea to save this....

			//based on input file, 
			var selectedProjectInGraph = projects.First(proj => proj.ProjectId == selectedProject.ProjectId);
			var dependants = GetDependantsForProject(selectedProjectInGraph);

			Console.WriteLine("Dependent Solutions");
			foreach (var solution in dependants.Item1.Distinct())
			{
				Console.WriteLine(solution.FilePath);
			}

			Console.WriteLine("Depenent Projects");
			foreach (var project in dependants.Item2.Distinct())
			{
				Console.WriteLine(project.AssemblyName);
			}
#if DEBUG
			Console.ReadLine();
#endif
		}

		private static Tuple<SolutionFile[], ProjectFile[]> GetDependantsForProject(ProjectFile inputProject)
		{
			var solutions = new List<SolutionFile>(inputProject.ReferencedBySolutions);
			var projects = new List<ProjectFile>(inputProject.ReferencedByProjects);

			foreach (var dependentProject in inputProject.ReferencedByProjects)
			{
				var dependants = GetDependantsForProject(dependentProject);
				solutions.AddRange(dependants.Item1);
				projects.AddRange(dependants.Item2);
			}

			return new Tuple<SolutionFile[], ProjectFile[]>(solutions.ToArray(), projects.ToArray());
		}

		private static ProjectFile BuildProjectFromFile(string filePath)
		{
			var inputText = File.ReadAllText(filePath);

			int nonIndex = 0;
			var projectIdString = GetBetween(inputText, @"<ProjectGuid>", @"</ProjectGuid>", ref nonIndex);
			var projectId = Guid.Empty;
			if (projectIdString != null)
			{
				projectId = Guid.Parse(projectIdString.Replace("{", "").Replace("}", ""));
			}


			nonIndex = 0;
			var assemblyName = GetBetween(inputText, @"<AssemblyName>", @"</AssemblyName>", ref nonIndex);

			int index = 0;
			var referenceIds = new List<Guid>();
			while (index < inputText.Length)
			{
				var projectRefText = GetBetween(inputText, @"<ProjectReference", @"</ProjectReference>", ref index);

				if (projectRefText != null)
				{
					//example
					//<ProjectReference Include="..\SMT.Utilities.Configuration\SMT.Utilities.Configuration.csproj">
					//  <Project>{906aa950-a547-4061-a64a-2a5181b01fed}</Project>
					//  <Name>SMT.Utilities.Configuration</Name>
					//</ProjectReference>
					int temp = 0;//ugh regretting that ref
					var projectIdText = GetBetween(projectRefText, @"<Project>{", @"}</Project>", ref temp);
					if (projectIdText != null)
					{
						referenceIds.Add(Guid.Parse(projectIdText));
					}
				}
			}

			return new ProjectFile()
			{
				FilePath = filePath,
				ProjectId = projectId,
				AssemblyName = assemblyName,
				ReferencedByProjects = new List<ProjectFile>(),
				ReferencedBySolutions = new List<SolutionFile>(),
				ReferencesProjectIds = referenceIds.ToArray(),
				ReferencesProjects = new List<ProjectFile>(),
			};
		}

		private static SolutionFile BuildSolutionFromFile(string filePath, ProjectFile[] projectList)
		{
			var inputText = File.ReadAllText(filePath);

			var solution = new SolutionFile()
			{
				FilePath = filePath,
				Projects = new List<ProjectFile>()
			};

			int index = 0; var done = false;
			while (index < inputText.Length && !done)
			{
				var projectString = GetBetween(inputText, "Project", "EndProject", ref index);
				if (projectString != null)
				{
					//Parsing this: ("typeGuid") = "NAME", "path", "IDGuid"
					var idGuidStr = projectString
										.Split(',')[2]
										.Split('\"')[1]
										.Replace("\"", "")
										.Trim();

					var projectGuid = Guid.Parse(idGuidStr);
					var dependentProjects = projectList.Where(proj => proj.ProjectId == projectGuid);
					solution.Projects.AddRange(dependentProjects);
					foreach (var project in dependentProjects)
					{
						project.ReferencedBySolutions.Add(solution);
					}
				}
			}

			return solution;
		}

		private static string GetBetween(string input, string start, string end, ref int index)
		{
			var firstInstanceOfStart = input.IndexOf(start, index);
			var firstInstanceOfEnd = input.IndexOf(end, firstInstanceOfStart + end.Length);

			if (firstInstanceOfStart > 0 && firstInstanceOfEnd > 0)
			{
				var startCapture = firstInstanceOfStart + start.Length;
				var capture = input.Substring(startCapture, firstInstanceOfEnd - startCapture);
				index = firstInstanceOfEnd + end.Length;
				return capture;
			}

			index = input.Length;
			return null;
		}


		private struct SolutionFile
		{
			public string FilePath;
			public List<ProjectFile> Projects; //guid identifier
		}

		private struct ProjectFile
		{
			public string FilePath;
			public string AssemblyName;
			public Guid[] ReferencesProjectIds; //intermediate step before graph building
			public List<ProjectFile> ReferencesProjects;
			public List<ProjectFile> ReferencedByProjects;
			public List<SolutionFile> ReferencedBySolutions;
			public Guid ProjectId; //guid identifier
		}
	}
}
