using Microsoft.AspNetCore.Mvc;
using PoC.TestWServ2.Common.Entities;
using PoC.TestWSrv2.BusinessLogic;

namespace PoC.TestWSrv2.WebServices.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityDocumentTypeController : ControllerBase
{
    private readonly IIdentityDocumentTypeService _identityDocumentTypeService;

    public IdentityDocumentTypeController(IIdentityDocumentTypeService identityDocumentTypeService)
    {
        _identityDocumentTypeService = identityDocumentTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IdentityDocumentType>>> GetAll()
    {
        var types = await _identityDocumentTypeService.GetAllIdentityDocumentTypesAsync();
        return Ok(types);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IdentityDocumentType>> Get(int id)
    {
        var type = await _identityDocumentTypeService.GetIdentityDocumentTypeByIdAsync(id);
        if (type == null)
            return NotFound();

        return Ok(type);
    }

    [HttpPost]
    public async Task<ActionResult<IdentityDocumentType>> Create([FromBody] IdentityDocumentType identityDocumentType)
    {
        try
        {
            var createdType = await _identityDocumentTypeService.CreateIdentityDocumentTypeAsync(identityDocumentType);
            return CreatedAtAction(nameof(Get), new { id = createdType.Id }, createdType);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IdentityDocumentType>> Update(int id, [FromBody] IdentityDocumentType identityDocumentType)
    {
        if (id != identityDocumentType.Id)
            return BadRequest("ID mismatch");

        try
        {
            var updatedType = await _identityDocumentTypeService.UpdateIdentityDocumentTypeAsync(identityDocumentType);
            return Ok(updatedType);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _identityDocumentTypeService.DeleteIdentityDocumentTypeAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}