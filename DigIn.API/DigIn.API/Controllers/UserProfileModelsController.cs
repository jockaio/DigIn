using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DigIn.API.Models;
using Microsoft.AspNet.Identity;

namespace DigIn.API.Controllers
{
    [Authorize]
    public class UserProfileModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/UserProfileModels
        [ResponseType(typeof(UserProfileModel))]
        public async Task<IHttpActionResult> GetUserProfileModel()
        {
            var currentUserId = User.Identity.GetUserId();
            UserProfileModel userProfileModel = await db.Users.Where(x => x.Id == currentUserId).Select(x => x.UserProfile).FirstAsync();
            if (userProfileModel == null)
            {
                return NotFound();
            }

            return Ok(userProfileModel);
        }

        // PUT: api/UserProfileModels
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserProfileModel(UserProfileModel userProfileModel)
        {
            var currentUserId = User.Identity.GetUserId();
            var id = await db.Users.Where(x => x.Id == currentUserId).Select(x => x.UserProfile.ID).FirstAsync();

            //ApplicationDbContext skillsContext = new ApplicationDbContext();
            //foreach (var item in (userProfileModel.Skills))
            //{
            //    if (item.ID == 0)
            //    {
            //        skillsContext.Skills.Add(item);
            //    }
            //    else
            //    {
            //        skillsContext.Entry(item).State = EntityState.Modified;
            //    }

            //}
            //await skillsContext.SaveChangesAsync();

            foreach (var item in userProfileModel.Skills)
            {
                item.UserProfileModelID = userProfileModel.ID;
                if (item.ID == 0)
                {
                    db.Entry(item).State = EntityState.Added;
                }
                else
                {
                    db.Entry(item).State = EntityState.Modified;
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userProfileModel.ID)
            {
                return BadRequest();
            }

            db.Entry(userProfileModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileModelExists(id))
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

        // POST: api/UserProfileModels
        [ResponseType(typeof(UserProfileModel))]
        public async Task<IHttpActionResult> PostUserProfileModel(UserProfileModel userProfileModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserProfileModels.Add(userProfileModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userProfileModel.ID }, userProfileModel);
        }

        // DELETE: api/UserProfileModels/5
        [ResponseType(typeof(UserProfileModel))]
        public async Task<IHttpActionResult> DeleteUserProfileModel(int id)
        {
            UserProfileModel userProfileModel = await db.UserProfileModels.FindAsync(id);
            if (userProfileModel == null)
            {
                return NotFound();
            }

            db.UserProfileModels.Remove(userProfileModel);
            await db.SaveChangesAsync();

            return Ok(userProfileModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserProfileModelExists(int id)
        {
            return db.UserProfileModels.Count(e => e.ID == id) > 0;
        }
    }
}