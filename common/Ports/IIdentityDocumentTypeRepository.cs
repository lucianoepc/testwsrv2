using PoC.TestWServ2.Common.Entities;

namespace PoC.TestWServ2.Common.Ports;

public interface IIdentityDocumentTypeRepository
{
    Task<IdentityDocumentType?> GetByIdAsync(int id);
    Task<IEnumerable<IdentityDocumentType>> GetAllAsync();
    Task<IdentityDocumentType> CreateAsync(IdentityDocumentType identityDocumentType);
    Task<IdentityDocumentType> UpdateAsync(IdentityDocumentType identityDocumentType);
    Task<bool> DeleteAsync(int id);
}