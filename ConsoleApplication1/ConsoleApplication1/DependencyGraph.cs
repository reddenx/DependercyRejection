using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Serializable]
    class DependencyGraph
    {
        public ProjectFile[] ProjectFiles;
        public SolutionFile[] SolutionFiles;

        public DependencyGraph(ProjectFile[] projects, SolutionFile[] solutions)
        {
            this.ProjectFiles = projects;
            this.SolutionFiles = solutions;
        }
    }
}
