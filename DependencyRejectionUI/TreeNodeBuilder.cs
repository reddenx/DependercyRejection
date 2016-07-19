using AltSource.Utilities.VSSolution;
using AltSource.Utilities.VSSolution.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DependercyRejectionUI
{
    public class RollupNode
    {
        Tuple<TreeNode, int> _tuple;
        public RollupNode(TreeNode node, int count)
        {
            _tuple = new Tuple<TreeNode, int>(node, count);
        }
        public TreeNode Node { get { return _tuple.Item1; } }
        public int ChildCount { get { return _tuple.Item2; } }
    }

    static class TreeNodeBuilder
    {
        public static RollupNode BuildTreeNodes(ProjectFile file, List<IGraphFilter> displayFilters, bool traverseUp)
        {
            foreach (var filter in displayFilters)
            {
                if (!filter.IsVisible(file))
                    return null;
            }

            var relatedProjects = (traverseUp) ? file.ReferencedByProjects : file.ReferencesProjects;

            int projectCount = relatedProjects.Count;

            var thisNode = new TreeNode()
            {
                Text = string.Format("{0}: {1} - [{2}]", 
                    file.AssemblyName, 
                    (0 == projectCount) ? string.Empty : projectCount.ToString(),
                    file.ProjectType.TypeName
                    )
            };

            foreach (var project in relatedProjects.OrderBy(proj => proj.AssemblyName))
            {
                var relatedTreeNode = BuildTreeNodes(project, displayFilters, traverseUp);
                if (relatedTreeNode != null)
                {
                    thisNode.Nodes.Add(relatedTreeNode.Node);
                    projectCount += relatedTreeNode.ChildCount;
                }
            }

            return new RollupNode(thisNode, projectCount);
        }
    }
}
