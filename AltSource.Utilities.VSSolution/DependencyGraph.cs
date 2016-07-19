using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution
{
    [Serializable]
    public class DependencyGraph
    {
        public ProjectFile[] ProjectFiles;
        public SolutionFile[] SolutionFiles;

        public DependencyGraph(ProjectFile[] projects, SolutionFile[] solutions)
        {
            this.ProjectFiles = projects;
            this.SolutionFiles = solutions;
        }

        public ProjectFile FindFileByName(string fileProjectName)
        {
            if (!fileProjectName.ToLower().EndsWith(".csproj"))
                fileProjectName += ".csproj";

            return ProjectFiles.Where(p => string.Compare(Path.GetFileName(p.FilePath), fileProjectName, true) == 0).FirstOrDefault();
        }
    }
}
