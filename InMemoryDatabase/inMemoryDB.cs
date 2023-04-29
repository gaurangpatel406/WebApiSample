using Class2WebApi.Exceptions;
using Class2WebApi.Models;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Class2WebApi.InMemoryDatabase
{
    public class inMemoryDB
    {

        private readonly List<Profile> _profiles = new List<Profile>();

        public void Create(Profile profile)
        {
            if (_profiles.Any(p => p.Id == profile.Id))
            {
                throw new DuplicateException($"A profile with same Id # ({profile.Id}) exists");//constructor from duplicateexception class 
            }
            if (string.IsNullOrEmpty(profile.Name))
            {
                throw new InvalidObjectException("The name cannot be empty");//constructor from invalidobjectexception class 
            }
            _profiles.Add(profile);

        }
        public IEnumerable<Profile> Get() 
        { 
            return _profiles;
        }
        public void Update(Profile profile)
        {
            var currentProfile = _profiles.FirstOrDefault(p => p.Id == profile.Id);
            if (currentProfile == null)
            {
                throw new NotFoundException("Not Found");//constructor from notfound class 
            }
            currentProfile.Name = profile.Name;
        }
        public void Delete(int id)
        {
            var profileToDelete = _profiles.FirstOrDefault(p => p.Id == id);
            if (profileToDelete == null)
            {
                throw new NotFoundException("Not Found");
            }
            _profiles.Remove(profileToDelete);
            
        }

        public Profile Get(int id)
        {
            var profile = _profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
            {
                throw new NotFoundException("Not Found");
            }
            return profile;
        }
    }
}
