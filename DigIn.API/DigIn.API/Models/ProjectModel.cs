using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigIn.API.Models
{
    public class ProjectModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectOwnerID { get; set; }
        public ApplicationUser ProjectOwner { get; set; }
        public List<ProjectContributor> ProjectContributors { get; set; }
    }

    public class ProjectContributor
    {
        public int ID { get; set; }
        public ApplicationUser User { get; set; }
        public ProjectRole ProjectRole { get; set; }
    }

    public enum ProjectRole
    {
        Designer,
        Developer
    }
}