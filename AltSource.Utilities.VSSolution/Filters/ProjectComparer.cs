using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution.Filters
{
    public class ProjectComparer : IEqualityComparer<ProjectFile>
    {
        public bool Equals(ProjectFile x, ProjectFile y)
        {
            return x.ProjectId == y.ProjectId;
        }

        public int GetHashCode(ProjectFile pData)
        {
            return pData.ProjectId.GetHashCode();
        }
    }
}
