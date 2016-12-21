using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DigIn.API.Models
{
    public class ProjectModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectOwnerID { get; set; }
        public UserProfileModel ProjectOwner { get; set; }
        public List<ProjectContributor> ProjectContributors { get; set; }
    }

    public class ProjectContributor
    {
        public int ID { get; set; }
        public UserProfileModel User { get; set; }
        public ProjectRole ProjectRole { get; set; }
    }

    public enum ProjectRole
    {
        Owner,
        ProjectLeader,
        Designer,
        Developer
    }

}