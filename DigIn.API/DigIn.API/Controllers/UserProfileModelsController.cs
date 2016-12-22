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
using System.Collections.Generic;

namespace DigIn.API.Controllers
{
    [Authorize]
    public class UserProfileModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/UserProfileModels
        [ResponseType(typeof(UserProfileModel))]
        public async Task<IHttpActionResult> GetUserProfileModel(int ID = 0)
        {
            UserProfileModel userProfileModel = new UserProfileModel();
            if (ID == 0)
            {
                var currentUserId = User.Identity.GetUserId();
                userProfileModel = await db.Users.Where(x => x.Id == currentUserId).Select(x => x.UserProfile).FirstAsync();
            }
            else
            {
                userProfileModel = await db.UserProfileModels.Where(x => x.ID == ID).FirstAsync();
            }

            //Check if user profile is found
            if (userProfileModel == null)
            {
                return NotFound();
            }

            //Check if user is allowed to edit the profile.
            userProfileModel.UserMatch = UserProfileMatch(userProfileModel.ID);

            return Ok(userProfileModel);
        }

        // PUT: api/UserProfileModels
        [ResponseType(typeof(UserProfileModel))]
        public async Task<IHttpActionResult> PutUserProfileModel(UserProfileModel userProfileModel)
        {
            if (!UserProfileMatch(userProfileModel.ID))
            {
                return BadRequest("User not allowed to edit profile.");
            }
            var currentUserId = User.Identity.GetUserId();
            var id = await db.Users.Where(x => x.Id == currentUserId).Select(x => x.UserProfile.ID).FirstAsync();

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

            var result = await db.UserProfileModels.Where(x => x.ID == userProfileModel.ID).FirstAsync();

            return Ok(result);
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

        private bool UserProfileMatch(int ID)
        {
            var currentUserId = User.Identity.GetUserId();
           
            return db.Users.Where(x => x.Id == currentUserId).Select(x => x.UserProfile.ID).First() == ID;
        }
    }
}