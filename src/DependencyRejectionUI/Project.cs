using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependercyRejectionUI
{
    public class Project
    {
        public Project(Guid? projectType)
        {
            projectType = ProjectType;
        }

        public string Name { get; set; }

        public Guid? ProjectType { get; private set; }

        public string Path { get; set; }

    }
}
