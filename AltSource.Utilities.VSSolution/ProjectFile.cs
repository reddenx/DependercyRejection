using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AltSource.Utilities.VSSolution.Filters;

namespace AltSource.Utilities.VSSolution
{
    [Serializable]
    public class ProjectFile
    {
        protected List<ProjectFile> _ancestors; 

        public string FilePath { get; protected set; }
        public string AssemblyName { get; protected set; }
        public ProjectOutputType OutputType { get; protected set; }
        public List<Guid> ReferencesProjectIds { get; protected set; } //intermediate step before graph building
        public List<ProjectFile> ReferencesProjects { get; protected set; }
        public List<ProjectFile> ReferencedByProjects { get; protected set; }
        public List<SolutionFile> ReferencedBySolutions { get; protected set; }
        public Guid ProjectId { get; protected set; } //guid identifier

        public XDocument Xml { get; protected set; }

        /// <summary>
        /// Microsoft project identifier
        /// </summary>
        public ProjectType ProjectType { get; protected set; }
        

        private ProjectFile(string filePath)
        {
            FilePath = filePath;
            ReferencedByProjects = new List<ProjectFile>();
            ReferencedBySolutions = new List<SolutionFile>();
            ReferencesProjects = new List<ProjectFile>();
        }

        public override string ToString()
        {
            return this.FilePath;
        }

        public static ProjectFile Build(Guid projectId)
        {
            var projFIle =  new ProjectFile(string.Empty);
            projFIle.ProjectId = projectId;
            return projFIle;
        }

        public static ProjectFile Build(string path) 
        {
            var projectFile = new ProjectFile(path);

            projectFile.Xml = XDocument.Load(path);

            var projectIdString = (projectFile.Xml.Descendants()
                .Where(a => a.Name.LocalName == "ProjectGuid")
                .FirstOrDefault() ?? new XElement("bunk"))
                .Value;
            var projectId = Guid.Empty;
            if (! string.IsNullOrEmpty(projectIdString))
            {
                projectFile.ProjectId = Guid.Parse(projectIdString.Replace("{", "").Replace("}", ""));
            }

            projectFile.AssemblyName = (projectFile.Xml.Descendants()
                .Where(a => a.Name.LocalName == "AssemblyName")
                .FirstOrDefault() ?? new XElement("bunk"))
                .Value;

            projectFile.ReferencesProjectIds = projectFile.Xml.Descendants()
                .Where(a => a.Name.LocalName == "ProjectReference")
                .SelectMany(pr => pr.Descendants().Where(p => p.Name.LocalName == "Project"))
                .Where(p => !string.IsNullOrWhiteSpace(p.Value))
                .Select(p => Guid.Parse(p.Value))
                .ToList();


            var outputTypeString = (projectFile.Xml.Descendants()
                .Where(a => a.Name.LocalName == "OutputType")
                .FirstOrDefault() ?? new XElement("bunk"))
                .Value;

            projectFile.OutputType = string.IsNullOrEmpty(outputTypeString) ? 
                            ProjectOutputType.Library : 
                            (ProjectOutputType)Enum.Parse(typeof(ProjectOutputType), outputTypeString);


            var projectTypeElement = projectFile.Xml.Descendants().Where(d => "ProjectTypeGuids" == d.Name.LocalName).FirstOrDefault();
            
            if (projectTypeElement != null)
            {
                var primaryGuid = projectTypeElement.Value.Split(new[] { ';' })
                   .Select(p => new Guid(p))
                   .ToArray()
                   [0];
                if (ProjectTypeDict.Contains(primaryGuid))
                {
                    projectFile.ProjectType = ProjectTypeDict.Get(primaryGuid);
                }
                else
                {
                    projectFile.ProjectType = ProjectTypeDict.Get(Guid.Empty);

                }
            }
            else
            {
                projectFile.ProjectType = ProjectTypeDict.Get(Guid.Empty);
            }
            return projectFile;
        }

        public int AddReference(ProjectFile referencedProject)
        {
            try
            {
                //update local XML file
                var relativePath = this.FilePath.GetRelativePathTo(referencedProject.FilePath);

                XElement ItemGroup = null;

                var ns = this.Xml.Root.GetDefaultNamespace();

                var newProjectElement = new XElement(ns+ "ProjectReference",
                    new XAttribute("Include", relativePath),
                    new XElement(ns + "Project", referencedProject.ProjectId.ToString("B")),
                    new XElement(ns + "Name", referencedProject.AssemblyName));
                
                int removed = RemoveExistingProjectReferences(referencedProject.ProjectId);

                var itemGroup = GetProjectItemGroupElemment();
                itemGroup.Add(newProjectElement);
                

                this.ReferencesProjectIds.Add(referencedProject.ProjectId);
                this.ReferencesProjects.Add(referencedProject);

                this.Xml.Save(this.FilePath, SaveOptions.OmitDuplicateNamespaces );

                return removed;

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// REturn all Solution files that should include me but don't
        /// </summary>
        public IEnumerable<SolutionFile> GetDeadBeatSolutionFiles()
        {
            var allSolutionsThatShouldReferenceMe = WhosMyDaddys();

            return allSolutionsThatShouldReferenceMe.Where(s => !s.Projects.Contains(this, new ProjectComparer()));
        }
        /// <summary>
        /// Return all Solution Files that should include me.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SolutionFile> WhosMyDaddys()
        {
            return this.GetAncestors().SelectMany(p => p.ReferencedBySolutions).Distinct(new SolutionComparer());
        }

        public IEnumerable<ProjectFile> GetAncestors()
        {
            _ancestors = new List<ProjectFile>();
            return AddUniqueAncestors(new ProjectFile[] {this});
        }

        protected IEnumerable<ProjectFile> AddUniqueAncestors(IEnumerable<ProjectFile> inputProjects)
        {
            //Add unique files
            _ancestors.AddRange(inputProjects.Where(i => !_ancestors.Any(a => a.ProjectId == i.ProjectId)));

            var referencingProjects = new List<ProjectFile>(inputProjects.SelectMany(proj => proj.ReferencedByProjects));

            if (referencingProjects.Count > 0)
            {   
            AddUniqueAncestors(referencingProjects);
            }

            return _ancestors;
        }

        /// <summary>
        /// Remove me from all solution files where I appear
        /// </summary>
        public IEnumerable<SolutionFile> Emancipate()
        {
            return Emancipate(WhosMyDaddys());
        }

        public IEnumerable<SolutionFile> Emancipate(IEnumerable<SolutionFile> solutionFiles)
        {
            var cleaned = new List<SolutionFile>();
            foreach (var solutionFile in solutionFiles)
            {
                if (solutionFile.RemoveProjectFileFromSolution(this))
                {
                    cleaned.Add(solutionFile);
                }
            }

            return cleaned;
        }


        protected int RemoveExistingProjectReferences(Guid projectId)
        {
            var proj = Xml.Descendants()
                .Where(a => a.Name.LocalName == "Project" && string.Compare(a.Value, projectId.ToString("B"), true) == 0 )
                ;
            int ct = proj.Count();

            proj.Remove();

            return ct;
        }

        protected XElement GetProjectItemGroupElemment()
        {
            var ns = Xml.Root.GetDefaultNamespace();

            var someOtherProjectReference = this.Xml.Descendants()
                     .Where(a => a.Name.LocalName == "ProjectReference")
                    .FirstOrDefault();

            if (null != someOtherProjectReference)
            {
                return someOtherProjectReference.Parent;
            }

            var newItemGroup = new XElement(ns + "ItemGroup");

            Xml.Root.Add(newItemGroup);

            return newItemGroup;
        }

        public override bool Equals(object obj)
        {
            return this.ProjectId == ((ProjectFile)obj).ProjectId;
        }

        public override int GetHashCode()
        {
            return this.ProjectId.GetHashCode();
        }
    }
}
