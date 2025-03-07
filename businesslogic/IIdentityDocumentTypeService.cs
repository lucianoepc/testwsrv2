using PoC.TestWServ2.Common.Entities;

namespace PoC.TestWSrv2.BusinessLogic;

public interface IIdentityDocumentTypeService
{
    Task<IdentityDocumentType?> GetIdentityDocumentTypeByIdAsync(int id);
    Task<IEnumerable<IdentityDocumentType>> GetAllIdentityDocumentTypesAsync();
    Task<IdentityDocumentType> CreateIdentityDocumentTypeAsync(IdentityDocumentType identityDocumentType);
    Task<IdentityDocumentType> UpdateIdentityDocumentTypeAsync(IdentityDocumentType identityDocumentType);
    Task<bool> DeleteIdentityDocumentTypeAsync(int id);
}