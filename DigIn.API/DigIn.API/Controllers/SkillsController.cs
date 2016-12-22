using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DigIn.API.Models;
using Microsoft.AspNet.Identity;


namespace DigIn.API.Controllers
{
    [Authorize]
    public class SkillsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Skills/{id}
        [Route("api/Skills/{id}")]
        [ResponseType(typeof(IQueryable<Skill>))]
        public  IQueryable<Skill> GetSkills(int ID)
        {
            if (ID == 0)
            {
                var currentUserId = User.Identity.GetUserId();
                ID = db.Users.Where(x => x.Id == currentUserId).Select(x => x.UserProfile.ID).First();
            }

            var result = db.Skills.Where(s => s.UserProfileModelID == ID);

            return result;
        }

        // GET: api/Skills/5
        [ResponseType(typeof(Skill))]
        public async Task<IHttpActionResult> GetSkill(int id)
        {
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        // PUT: api/Skills/5
        [ResponseType(typeof(void))]
        [Route("api/Skills/{id}")]
        public async Task<IHttpActionResult> PutSkill(int id, Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skill.ID)
            {
                return BadRequest();
            }

            db.Entry(skill).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Skills
        [ResponseType(typeof(void))]
        [Route("api/Skills")]
        public async Task<IHttpActionResult> PostSkill(Skill skill)
        {
            var currentUserId = User.Identity.GetUserId();
            skill.UserProfileModelID = db.Users.Where(x => x.Id == currentUserId).Select(x => x.UserProfile.ID).First();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Skills.Add(skill);
            await db.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Skills/5
        [ResponseType(typeof(List<Skill>))]
        [Route("api/Skills/{id}")]
        public async Task<IHttpActionResult> DeleteSkill(int id)
        {
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            db.Skills.Remove(skill);
            await db.SaveChangesAsync();

            var currentUserId = User.Identity.GetUserId();
            var skills = await db.Users.Where(x => x.Id == currentUserId).Select(x => x.UserProfile.Skills).FirstAsync();

            return Ok(skills);
        }

        // GET: api/UserProfileModels
        [ResponseType(typeof(IQueryable<SkillsCategory>))]
        [Route("api/SkillsCategories")]
        public async Task<IHttpActionResult> GetSkillsCategories()
        {
            var result = await db.SkillsCategories.ToListAsync();
            //Check if user profile is found
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkillExists(int id)
        {
            return db.Skills.Count(e => e.ID == id) > 0;
        }
    }
}