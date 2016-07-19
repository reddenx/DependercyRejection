using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependercyRejectionUI
{
    public class ProjectLibrary: List<Project>
    {
        protected List<IDisplayFilter> _filters;

        public ProjectLibrary()
        {
            _filters = new List<IDisplayFilter>();
        }

        public void AddFilter(IDisplayFilter newFilter)
        {
            if (!_filters.Exists(f => f.GetType() == newFilter.GetType() && f.FilterValue == newFilter.FilterValue))
            {
                _filters.Add(newFilter);
            }
        }

        public bool RemoveFilter(IDisplayFilter oldFilter)
        {
            _filters.Remove(oldFilter);
        }

        public List<Project> GetFiltered()
        {

        }
    }
}
