using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DigIn.API.Models
{
    public class UserProfileModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public virtual List<Skill> Skills { get; set; }
        public virtual List<Experience> Experiences { get; set; }
        [NotMapped]
        public bool UserMatch { get; set; }
    }

    public class Skill
    {
        public int ID { get; set; }
        public int SkillsCategoryID { get; set; }
        [ForeignKey("SkillsCategoryID")]
        public SkillsCategory SkillsCategory { get; set; }
        public string Description { get; set; }
        public int UserProfileModelID { get; set; }

        [ForeignKey("UserProfileModelID")]
        public UserProfileModel UserProfileModel { get; set; }
    }

    public class Experience
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectModel Project { get; set; }
    }

    public class SkillsCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }

    public enum Category
    {
        Programmering,
        Projektledning,
        Bildredigering
    }
}