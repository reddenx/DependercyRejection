using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Filters
{
    public class ProjectTypeFilter: IGraphFilter
    {
        List<Guid> _projectFilters;

        public ProjectTypeFilter()
        {
            _projectFilters = new List<Guid>();
        }

        public bool IsVisible(ProjectFile project)
        {
            var dummy = new Guid();
            return (!_projectFilters.Contains(project.ProjectType?? dummy));
        }
    }
}
