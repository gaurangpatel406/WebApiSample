using Class2WebApi.Models;

namespace Class2WebApi.InMemoryDatabase
{
    public interface IDatabase
    {
        void Create(Profile profile);
        void Delete(int id);
        IEnumerable<Profile> Get();
        Profile Get(int id);
        void Update(Profile profile);
    }
}