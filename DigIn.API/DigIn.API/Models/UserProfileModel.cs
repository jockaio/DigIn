using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigIn.API.Models
{
    public class UserProfileModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Experience> Experiences { get; set; }
    }

    public class Skill
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Experience
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectModel Project { get; set; }
    }
}