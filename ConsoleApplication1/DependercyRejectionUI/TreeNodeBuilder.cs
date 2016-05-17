using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DependercyRejectionUI
{
    static class TreeNodeBuilder
    {
        public static Tuple<TreeNode, int> BuildTreeNodesDown(ProjectFile file, ProjectFile? filter)
        {
            var thisNode = new TreeNode();

            if (filter.HasValue)
            {
                if (filter.Value.AssemblyName == file.AssemblyName)
                {
                    thisNode.Text = file.AssemblyName;
                    return new Tuple<TreeNode, int>(thisNode, 1);
                }
                else if (!file.ReferencesProjects.Any())
                {
                    return null;
                }
            }

            int projectCount = 0;
            foreach (var project in file.ReferencesProjects.OrderBy(proj => proj.AssemblyName))
            {
                var info = BuildTreeNodesDown(project, filter);
                if (info != null)
                {
                    thisNode.Nodes.Add(info.Item1);
                    projectCount += info.Item2;
                }
            }

            if (filter.HasValue)
            {
                if (thisNode.Nodes.Count == 0)
                {
                    return null;
                }
            }
            else
            {
                projectCount += thisNode.Nodes.Count;
            }

            thisNode.Text = (string.Format("{0}: {1}", file.AssemblyName, projectCount));

            return new Tuple<TreeNode, int>(thisNode, projectCount);
        }

        public static Tuple<TreeNode, int> BuildTreeNodesUp(ProjectFile file, ProjectFile? filter)
        {
            var thisNode = new TreeNode();

            if (filter.HasValue)
            {
                if (filter.Value.AssemblyName == file.AssemblyName)
                {
                    thisNode.Text = file.AssemblyName;
                    return new Tuple<TreeNode, int>(thisNode, 1);
                }
                else if (!file.ReferencedByProjects.Any())
                {
                    return null;
                }
            }

            int projectCount = 0;
            foreach (var project in file.ReferencedByProjects.OrderBy(proj => proj.AssemblyName))
            {
                var info = BuildTreeNodesUp(project, filter);
                if (info != null)
                {
                    thisNode.Nodes.Add(info.Item1);
                    projectCount += info.Item2;
                }
            }

            if (filter.HasValue)
            {
                if (thisNode.Nodes.Count == 0)
                {
                    return null;
                }
            }
            else
            {
                projectCount += thisNode.Nodes.Count;
            }

            thisNode.Text = (string.Format("{0}: {1}", file.AssemblyName, projectCount));

            return new Tuple<TreeNode, int>(thisNode, projectCount);
        }
    }
}
