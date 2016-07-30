using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution.Filters
{
    public class ProjectIdFilter: IGraphFilter
    {
        public Guid FilterProjectGuid
        {
            get; set;
        }

        public bool IsVisible(ProjectFile project)
        {
            return project.ProjectId != FilterProjectGuid;
        }

        public bool Equals(IGraphFilter obj)
        {
            if (obj == null)
            {
                return false;
            }
            
            var other = obj as ProjectIdFilter;
            if (other == null)
            {
                return false;
            }
            
            return this.FilterProjectGuid == other.FilterProjectGuid;
        }
        
    }
}
