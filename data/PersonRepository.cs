using Microsoft.EntityFrameworkCore;
using PoC.TestWServ2.Common.Entities;
using PoC.TestWServ2.Common.Ports;

namespace PoC.TestWSrv2.Data;

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _context;

    public PersonRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _context.People.FindAsync(id);
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.People.ToListAsync();
    }

    public async Task<Person> CreateAsync(Person person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<Person> UpdateAsync(Person person)
    {
        _context.Entry(person).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null)
            return false;

        _context.People.Remove(person);
        await _context.SaveChangesAsync();
        return true;
    }
}