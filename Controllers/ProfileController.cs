using Class2WebApi.Exceptions;
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
            return Ok(_db.Get());

        }
        [HttpGet,Route("{id}")] //api/profiles/{id}
        //[HttpGet]   
        //[Route("{Id}")]
        //[HttpGet("{Id}"]
        public ActionResult GetAction(int id)
        {
            
            try
            {
                var profile = _db.Get(id);
                return Ok(profile);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public ActionResult Post(Profile profile)  //create - The profile var is bound to the body of the request
        {
            try
            {
                _db.Create(profile);
                //return ok();

                return Created($"api/profiles/{profile.Id}", profile);
            }
            catch (DuplicateException)
            {
                return Conflict();
            }
            catch(InvalidObjectException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public ActionResult update(Profile profile)
        {
            
            try
            {
                _db.Update(profile);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();

            }
        }
        [HttpDelete]//, Route("{id}")] //can user query parameter ?Id=3 in VS code delete if dont want to add route to delete action
        public ActionResult Delete(int id)
        {
           
            try
            {
                _db.Delete(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            

        }
    }
}
