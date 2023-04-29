using Class2WebApi.InMemoryDatabase;
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
       private inMemoryDB _db;
        public ProfileController(inMemoryDB db)
        {
            _db = db;
        }


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
            if (_profiles.Any(p=>p.Id==profile.Id))
            {
                return Conflict($"A profile with same Id # ({profile.Id}) exists");
            }
            if (string.IsNullOrEmpty(profile.Name))
            {
                return BadRequest("The name cannot be empty");
            }
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
