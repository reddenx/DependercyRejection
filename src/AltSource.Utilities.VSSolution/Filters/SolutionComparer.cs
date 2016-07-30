using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution.Filters
{
    public class SolutionComparer: IEqualityComparer<SolutionFile>
    {
        public bool Equals(SolutionFile x, SolutionFile y)
        {
            return x.FilePath == y.FilePath;
        }

        public int GetHashCode(SolutionFile pData)
        {
            return pData.FilePath.GetHashCode();
        }
    }
}
