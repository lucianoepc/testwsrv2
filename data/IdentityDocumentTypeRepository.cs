using Microsoft.EntityFrameworkCore;
using PoC.TestWServ2.Common.Entities;
using PoC.TestWServ2.Common.Ports;

namespace PoC.TestWSrv2.Data;

public class IdentityDocumentTypeRepository : IIdentityDocumentTypeRepository
{
    private readonly ApplicationDbContext _context;

    public IdentityDocumentTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IdentityDocumentType?> GetByIdAsync(int id)
    {
        return await _context.IdentityDocumentTypes.FindAsync(id);
    }

    public async Task<IEnumerable<IdentityDocumentType>> GetAllAsync()
    {
        return await _context.IdentityDocumentTypes.ToListAsync();
    }

    public async Task<IdentityDocumentType> CreateAsync(IdentityDocumentType identityDocumentType)
    {
        _context.IdentityDocumentTypes.Add(identityDocumentType);
        await _context.SaveChangesAsync();
        return identityDocumentType;
    }

    public async Task<IdentityDocumentType> UpdateAsync(IdentityDocumentType identityDocumentType)
    {
        _context.Entry(identityDocumentType).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return identityDocumentType;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var identityDocumentType = await _context.IdentityDocumentTypes.FindAsync(id);
        if (identityDocumentType == null)
            return false;

        _context.IdentityDocumentTypes.Remove(identityDocumentType);
        await _context.SaveChangesAsync();
        return true;
    }
}