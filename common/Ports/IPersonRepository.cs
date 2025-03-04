using PoC.TestWServ2.Common.Entities;

namespace PoC.TestWServ2.Common.Ports;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(int id);
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person> CreateAsync(Person person);
    Task<Person> UpdateAsync(Person person);
    Task<bool> DeleteAsync(int id);
}