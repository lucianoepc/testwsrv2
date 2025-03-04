using PoC.TestWServ2.Common.Entities;

namespace PoC.TestWSrv2.BusinessLogic;

public interface IPersonService
{
    Task<Person?> GetPersonByIdAsync(int id);
    Task<IEnumerable<Person>> GetAllPersonsAsync();
    Task<Person> CreatePersonAsync(Person person);
    Task<Person> UpdatePersonAsync(Person person);
    Task<bool> DeletePersonAsync(int id);
}