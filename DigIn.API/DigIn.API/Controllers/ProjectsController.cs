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
using Microsoft.AspNet.Identity;
using DigIn.API.Models;

namespace DigIn.API.Controllers
{
    [Authorize]
    public class ProjectsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Projects
        public IQueryable<ProjectModel> GetProjectModels()
        {
            var currentUserId = User.Identity.GetUserId();
            var userProfileId = db.Users.Where(u => u.Id == currentUserId).Select(up => up.UserProfile.ID).FirstOrDefault();
            var projectModels = db.ProjectModels.Where(p => p.ProjectOwnerID == userProfileId || p.ProjectContributors.Select(pc => pc.User.ID).Contains(userProfileId));

            return projectModels;
        }

        // GET: api/Projects/5
        [ResponseType(typeof(ProjectModel))]
        public async Task<IHttpActionResult> GetProjectModel(int id)
        {
            ProjectModel projectModel = await db.ProjectModels.FindAsync(id);
            if (projectModel == null)
            {
                return NotFound();
            }

            return Ok(projectModel);
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectModel(int id, ProjectModel projectModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectModel.ID)
            {
                return BadRequest();
            }

            db.Entry(projectModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectModelExists(id))
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

        // POST: api/Projects
        [ResponseType(typeof(ProjectModel))]
        public async Task<IHttpActionResult> PostProjectModel(ProjectModel projectModel)
        {
            var currentUserId = User.Identity.GetUserId();
            projectModel.ProjectOwner = await db.Users.Where(x => x.Id == currentUserId).Select(x => x.UserProfile).FirstAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectModels.Add(projectModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectModel.ID }, projectModel);
        }

        // DELETE: api/Projects/5
        [ResponseType(typeof(ProjectModel))]
        public async Task<IHttpActionResult> DeleteProjectModel(int id)
        {
            ProjectModel projectModel = await db.ProjectModels.FindAsync(id);
            if (projectModel == null)
            {
                return NotFound();
            }

            db.ProjectModels.Remove(projectModel);
            await db.SaveChangesAsync();

            return Ok(projectModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectModelExists(int id)
        {
            return db.ProjectModels.Count(e => e.ID == id) > 0;
        }
    }
}