using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AltSource.Utilities.VSSolution.Filters;

namespace AltSource.Utilities.VSSolution
{
    [Serializable]
    public class SolutionFile
    {
        public string FilePath;
        public List<ProjectFile> Projects; //guid identifier
        public string InputText { get; set; }

        public string FileName
        {
            get { return Path.GetFileName(this.FilePath); }
        }

<<<<<<< HEAD
        public static SolutionFile BuildFromFile(string filePath, List<ProjectFile> projectList)
=======
        public static SolutionFile BuildFromFile(string filePath, ProjectFile[] projectList)
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
        {
            var inputText = File.ReadAllText(filePath);

            var solution = new SolutionFile()
            {
                FilePath = filePath,
                Projects = new List<ProjectFile>(),
                InputText = inputText

            };

            int index = 0; var done = false;
            while (index < inputText.Length && !done)
            {
                var projectString = inputText.GetBetween("Project", "EndProject", ref index);
                if (projectString != null)
                {
                    var projectGuid = ParseProjectGuid(projectString);
<<<<<<< HEAD
                    var dependentProjects = projectList.Where(proj => proj.ProjectId == projectGuid).ToList();

                    if (dependentProjects.Count() == 0)
                    {
                        ProjectType projectType = ParseProjectType(projectString);
                        //Is solutionFolder
                        if (projectType.ID != Guid.Parse("2150E333-8FDC-42A3-9474-1A3956D46DE8"))
                        {
                            var assName = ParseProjectAssemblyName(projectString);
                            var stubProject = ProjectFile.Build(projectGuid, assName, projectType);
                            dependentProjects.Add(stubProject);
                            projectList.Add(stubProject);
                        }
                    }

=======
                    var dependentProjects = projectList.Where(proj => proj.ProjectId == projectGuid);
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
                    solution.Projects.AddRange(dependentProjects);
                    foreach (var project in dependentProjects)
                    {
                        project.ReferencedBySolutions.Add(solution);
                    }
                }
            }

            return solution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectFile"></param>
        /// <param name="addAsType">In case project doesn't know it's own type</param>
        /// <returns></returns>
        public bool AddProjectFileToSolution(ProjectFile projectFile, ProjectType addAsType)
        {
            //Adding this : ("typeGuid") = "NAME", "path", "IDGuid"
            //Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "CCI.Interfaces.Dependencies", "CCI.Interfaces.Dependencies\CCI.Interfaces.Dependencies.csproj", "{24C4E082-2D60-4434-998C-200FF15675DF}"
            //EndProject

            if (this.Projects.Contains(projectFile, new ProjectComparer()))
            {
                return false;
            }

            bool wroteProject = false;
            if (!this.Projects.Contains(projectFile, new ProjectComparer()))
            {
                var insertionPoint = 0;
                int index = 0;
                var done = false;
                while (index < InputText.Length && !done)
                {
                    var projectString = InputText.GetBetween("Project", "EndProject", ref index);
                    if (projectString != null)
                    {
                        var projectType = ParseProjectType(projectString);
                        if (projectType == null || 
                            (projectType.TypeName != "Solution Folder" &&
                            new Guid("2150E333-8FDC-42A3-9474-1A3956D46DE8") != projectType.ID))
                        {
                            done = true;
                        }
                    }
                }

                InputText = InputText.Insert(index, string.Format(@"
Project(""{{{0}}}"") = ""{1}"", ""{2}"",\ ""{{{3}}}""
EndProject",
                   addAsType.ID.ToString().ToUpper(), projectFile.AssemblyName, this.FilePath.GetRelativePathTo(projectFile.FilePath), projectFile.ProjectId.ToString().ToLower()));
                File.WriteAllText(this.FilePath, this.InputText);
            }

            return true;
        }


        public bool RemoveProjectFileFromSolution(ProjectFile projectFile)
        {
<<<<<<< HEAD
            var regexProjRef = new Regex(@"Project\(""{.+(?:" + projectFile.ProjectId.ToString() + @")}""\r\nEndProject\r\n", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            var regexConfigs = new Regex(@"^\s*\{" + projectFile.ProjectId.ToString() + @"\}.+", RegexOptions.IgnoreCase);

            if (regexProjRef.IsMatch(InputText))
            {
                InputText = regexProjRef.Replace(InputText, string.Empty);

                InputText = regexConfigs.Replace(InputText, String.Empty);

=======
            var regex = new Regex(@"Project\(""{.+(?:" + projectFile.ProjectId.ToString() + @")}""\r\nEndProject\r\n", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            if (regex.IsMatch(InputText))
            {
                InputText = regex.Replace(InputText, string.Empty);
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
                File.WriteAllText(this.FilePath, this.InputText);
                this.Projects.RemoveAll( p => p.ProjectId == projectFile.ProjectId);
                return true;
            }

            return false;
        }

        private static ProjectType ParseProjectType(string projectString)
        {
            var idGuidStr = projectString
                                .Split(',')[0]
                                .Split('=')[0]
                                .GetBetween("\"{", "}\"")
                                .Trim();
            var guid = Guid.Parse(idGuidStr);

            ProjectType projectType = null;
            if (ProjectTypeDict.Contains(guid))
            {
                projectType = ProjectTypeDict.Get(guid);
            }

            return projectType;
        }

        private static Guid ParseProjectGuid(string projectString)
        {
            //Parsing this: ("typeGuid") = "NAME", "path", "IDGuid"
            var idGuidStr = projectString
                                .Split(',')[2]
                                .Split('\"')[1]
                                .Replace("\"", "")
                                .Trim();

            return Guid.Parse(idGuidStr);
        }

<<<<<<< HEAD
        private static string ParseProjectAssemblyName(string projectString)
        {
            //Parsing this: ("typeGuid") = "NAME", "path", "IDGuid"
            var assName = projectString
                                .Split(',')[0]
                                .Split('=')[1]
                                .Replace("\"", "")
                                .Trim();

            return assName;
        }

=======
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
        public override bool Equals(object obj)
        {
            return this.FilePath == ((SolutionFile)obj).FilePath;
        }

        public override int GetHashCode()
        {
            return this.FilePath.GetHashCode();
        }
    }
}
