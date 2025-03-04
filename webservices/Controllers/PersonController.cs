using Microsoft.AspNetCore.Mvc;
using PoC.TestWServ2.Common.Entities;
using PoC.TestWSrv2.BusinessLogic;

namespace PoC.TestWSrv2.WebServices.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetAll()
    {
        var people = await _personService.GetAllPersonsAsync();
        return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> Get(int id)
    {
        var person = await _personService.GetPersonByIdAsync(id);
        if (person == null)
            return NotFound();

        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> Create([FromBody] Person person)
    {
        try
        {
            var createdPerson = await _personService.CreatePersonAsync(person);
            return CreatedAtAction(nameof(Get), new { id = createdPerson.Id }, createdPerson);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Person>> Update(int id, [FromBody] Person person)
    {
        if (id != person.Id)
            return BadRequest("ID mismatch");

        try
        {
            var updatedPerson = await _personService.UpdatePersonAsync(person);
            return Ok(updatedPerson);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _personService.DeletePersonAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}