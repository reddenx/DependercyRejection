using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Serializable]
    public struct ProjectFile
    {
        public string FilePath;
        public string AssemblyName;
        public string OutputType;
        public Guid[] ReferencesProjectIds; //intermediate step before graph building
        public List<ProjectFile> ReferencesProjects;
        public List<ProjectFile> ReferencedByProjects;
        public List<SolutionFile> ReferencedBySolutions;
        public Guid ProjectId; //guid identifier

        private ProjectFile(string filePath, Guid projectId, string assemblyName, Guid[] referenceIds, string outputType)
        {
            FilePath = filePath;
            ProjectId = projectId;
            AssemblyName = assemblyName;
            ReferencedByProjects = new List<ProjectFile>();
            ReferencedBySolutions = new List<SolutionFile>();
            ReferencesProjectIds = referenceIds.ToArray();
            ReferencesProjects = new List<ProjectFile>();
            OutputType = outputType;
        }

        public static ProjectFile BuildFromFile(string filePath)
        {
            var inputText = File.ReadAllText(filePath);

            int nonIndex = 0;
            var projectIdString = inputText.GetBetween(@"<ProjectGuid>", @"</ProjectGuid>", ref nonIndex);
            var projectId = Guid.Empty;
            if (projectIdString != null)
            {
                projectId = Guid.Parse(projectIdString.Replace("{", "").Replace("}", ""));
            }

            nonIndex = 0;
            var assemblyName = inputText.GetBetween(@"<AssemblyName>", @"</AssemblyName>", ref nonIndex);

            int index = 0;
            var referenceIds = new List<Guid>();
            while (index < inputText.Length)
            {
                var projectRefText = inputText.GetBetween(@"<ProjectReference", @"</ProjectReference>", ref index);

                if (projectRefText != null)
                {
                    int temp = 0;//ugh regretting that ref
                    var projectIdText = projectRefText.GetBetween(@"<Project>{", @"}</Project>", ref temp);
                    if (projectIdText != null)
                    {
                        referenceIds.Add(Guid.Parse(projectIdText));
                    }
                }
            }

            nonIndex = 0;
            var outputType = inputText.GetBetween(@"<OutputType>", @"</OutputType>", ref nonIndex);

            return new ProjectFile()
            {
                FilePath = filePath,
                ProjectId = projectId,
                AssemblyName = assemblyName,
                OutputType = outputType,
                ReferencedByProjects = new List<ProjectFile>(),
                ReferencedBySolutions = new List<SolutionFile>(),
                ReferencesProjectIds = referenceIds.ToArray(),
                ReferencesProjects = new List<ProjectFile>(),
            };
        }
    }
}
