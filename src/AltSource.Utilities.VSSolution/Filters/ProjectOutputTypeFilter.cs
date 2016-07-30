using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution.Filters
{
    public class ProjectOutputTypeFilter : IGraphFilter
    {
        public ProjectOutputType OutputType { get; set; }

        public bool Equals(IGraphFilter filter)
        {
            if (filter == null)
            {
                return false;
            }

            var other = filter as ProjectOutputTypeFilter;
            if (other == null)
            {
                return false;
            }

            return this.OutputType == other.OutputType;
        }

        public bool IsVisible(ProjectFile project)
        {
            return (this.OutputType != project.OutputType);
        }
    }
}
