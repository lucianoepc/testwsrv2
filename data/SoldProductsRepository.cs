using Microsoft.EntityFrameworkCore;
using PoC.TestWServ2.Common.Entities;
using PoC.TestWServ2.Common.Ports;

namespace PoC.TestWSrv2.Data;

public class SoldProductsRepository : ISoldProductsRepository
{
    private readonly ApplicationDbContext _context;

    public SoldProductsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SoldProducts>> GetByPersonIdAsync(int personId)
    {
        return await _context.SoldProducts
            .Include(sp => sp.Product)
            .Include(sp => sp.Person)
            .Where(sp => sp.PersonId == personId)
            .ToListAsync();
    }

    public async Task<IEnumerable<SoldProducts>> GetAllAsync()
    {
        return await _context.SoldProducts
            .Include(sp => sp.Product)
            .Include(sp => sp.Person)
            .ToListAsync();
    }

    public async Task<SoldProducts> CreateAsync(SoldProducts soldProduct)
    {
        _context.SoldProducts.Add(soldProduct);
        await _context.SaveChangesAsync();
        return soldProduct;
    }
}