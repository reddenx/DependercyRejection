using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution.Filters
{
    public class ProjectTypeFilter: IGraphFilter
    {
        public ProjectType ProjectType { get; set; }

        public bool IsVisible(ProjectFile project)
        {
            return (project.ProjectType.ID != this.ProjectType.ID);
        }

        public bool Equals(IGraphFilter obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as ProjectTypeFilter;
            if (other == null)
            {
                return false;
            }

            return this.ProjectType.ID == other.ProjectType.ID;
        }
    }
}
