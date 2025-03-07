using PoC.TestWServ2.Common.Entities;
using PoC.TestWServ2.Common.Ports;

namespace PoC.TestWSrv2.BusinessLogic;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;

    public PersonService(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Person?> GetPersonByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        // Convertir fechas a UTC
        person.DateOfBirth = DateTime.SpecifyKind(person.DateOfBirth, DateTimeKind.Utc);

        if (string.IsNullOrWhiteSpace(person.FirstName))
            throw new ArgumentException("First name is required", nameof(person));

        if (string.IsNullOrWhiteSpace(person.LastName))
            throw new ArgumentException("Last name is required", nameof(person));

        if (person.DateOfBirth == default)
            throw new ArgumentException("Date of birth is required", nameof(person));

        if (string.IsNullOrWhiteSpace(person.PlaceOfBirth))
            throw new ArgumentException("Place of birth is required", nameof(person));

        return await _repository.CreateAsync(person);
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        var existingPerson = await _repository.GetByIdAsync(person.Id);
        if (existingPerson == null)
            throw new ArgumentException($"Person with ID {person.Id} not found", nameof(person));

        if (string.IsNullOrWhiteSpace(person.FirstName))
            throw new ArgumentException("First name is required", nameof(person));

        if (string.IsNullOrWhiteSpace(person.LastName))
            throw new ArgumentException("Last name is required", nameof(person));

        if (person.DateOfBirth == default)
            throw new ArgumentException("Date of birth is required", nameof(person));

        if (string.IsNullOrWhiteSpace(person.PlaceOfBirth))
            throw new ArgumentException("Place of birth is required", nameof(person));

        return await _repository.UpdateAsync(person);
    }

    public async Task<bool> DeletePersonAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}