using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigInAPI.Models
{
    public class ProjectModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IdentityUser ProjectOwner { get; set; }
        public List<ProjectContributor> ProjectContributors { get; set; }

    }

    public class ProjectContributor
    {
        public IdentityUser User { get; set; }
        public ProjectRole ProjectRole { get; set; }
    }

    public enum ProjectRole
    {
        Developer,
        Tester,
        Designer
    }
}