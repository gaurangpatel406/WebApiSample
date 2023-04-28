using Class2WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Class2WebApi.Controllers
{
    [Route("api/[controller]")] //https://localhost:[Port]/api/Profiles
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private static readonly List<Profile> _profiles = new List<Profile>();
        
        [HttpGet]
        public  ActionResult<Profile> Get() //Read everrything
        {
            return Ok(_profiles);

        }
        [HttpGet,Route("{id}")] //api/profiles/{id}
        //[HttpGet]   
        //[Route("{Id}")]
        //[HttpGet("{Id}"]
        public ActionResult GetAction(int id)
        {
            var profile=_profiles.FirstOrDefault(p => p.Id == id);
            if(profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }
        [HttpPost]
        public ActionResult Post(Profile profile)  //create - The profile var is bound to the body of the request
        {
            _profiles.Add(profile);
            //return ok();
            return Created($"api/profiles/{profile.Id}", profile);
        }
        [HttpPut]
        public ActionResult update(Profile profile)
        {
            var currentProfile = _profiles.FirstOrDefault(p => p.Id == profile.Id);
            if(currentProfile == null)
            {
                return NotFound();

            }
            currentProfile.Name = profile.Name;
            return Ok();
        }
        [HttpDelete]//, Route("{id}")] //can user query parameter ?Id=3 in VS code delete if dont want to add route to delete action
        public ActionResult Delete(int id)
        {
            var profileToDelete =_profiles.FirstOrDefault(p => p.Id == id);
            if (profileToDelete == null)
            {
                return NotFound();
            }
            _profiles.Remove(profileToDelete);
            return Ok();

        }
    }
}
