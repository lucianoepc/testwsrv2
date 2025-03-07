using Microsoft.AspNetCore.Mvc;
using PoC.TestWServ2.Common.Entities;
using PoC.TestWSrv2.BusinessLogic;

namespace PoC.TestWSrv2.WebServices.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SoldProductsController : ControllerBase
{
    private readonly ISoldProductsService _soldProductsService;

    public SoldProductsController(ISoldProductsService soldProductsService)
    {
        _soldProductsService = soldProductsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SoldProducts>>> GetAll()
    {
        var soldProducts = await _soldProductsService.GetAllSoldProductsAsync();
        return Ok(soldProducts);
    }

    [HttpGet("person/{personId}")]
    public async Task<ActionResult<IEnumerable<SoldProducts>>> GetByPerson(int personId)
    {
        try
        {
            var soldProducts = await _soldProductsService.GetSoldProductsByPersonIdAsync(personId);
            return Ok(soldProducts);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<SoldProducts>> Create([FromBody] SoldProducts soldProduct)
    {
        try
        {
            var createdSoldProduct = await _soldProductsService.CreateSoldProductAsync(soldProduct);
            return CreatedAtAction(nameof(GetByPerson), new { personId = createdSoldProduct.PersonId }, createdSoldProduct);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}