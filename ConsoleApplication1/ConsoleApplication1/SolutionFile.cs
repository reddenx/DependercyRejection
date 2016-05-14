using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Serializable]
    public struct SolutionFile
    {
        public string FilePath;
        public List<ProjectFile> Projects; //guid identifier

        public SolutionFile(string filePath)
        {
            FilePath = filePath;
            Projects = new List<ProjectFile>();
        }

        public static SolutionFile BuildFromFile(string filePath, ProjectFile[] projectList)
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
                var projectString = inputText.GetBetween("Project", "EndProject", ref index);
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

    }
}
