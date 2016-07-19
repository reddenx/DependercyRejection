using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AltSource.Utilities.VSSolution.Filters
{
    public interface IGraphFilter: IEquatable<IGraphFilter>
    {
        bool IsVisible(ProjectFile project);
    }
}
