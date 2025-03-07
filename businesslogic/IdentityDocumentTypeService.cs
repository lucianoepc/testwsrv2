using PoC.TestWServ2.Common.Entities;
using PoC.TestWServ2.Common.Ports;

namespace PoC.TestWSrv2.BusinessLogic;

public class IdentityDocumentTypeService : IIdentityDocumentTypeService
{
    private readonly IIdentityDocumentTypeRepository _repository;

    public IdentityDocumentTypeService(IIdentityDocumentTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdentityDocumentType?> GetIdentityDocumentTypeByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<IdentityDocumentType>> GetAllIdentityDocumentTypesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IdentityDocumentType> CreateIdentityDocumentTypeAsync(IdentityDocumentType identityDocumentType)
    {
        if (string.IsNullOrWhiteSpace(identityDocumentType.Code))
            throw new ArgumentException("Code is required", nameof(identityDocumentType));

        if (string.IsNullOrWhiteSpace(identityDocumentType.Description))
            throw new ArgumentException("Description is required", nameof(identityDocumentType));

        return await _repository.CreateAsync(identityDocumentType);
    }

    public async Task<IdentityDocumentType> UpdateIdentityDocumentTypeAsync(IdentityDocumentType identityDocumentType)
    {
        if (string.IsNullOrWhiteSpace(identityDocumentType.Code))
            throw new ArgumentException("Code is required", nameof(identityDocumentType));

        if (string.IsNullOrWhiteSpace(identityDocumentType.Description))
            throw new ArgumentException("Description is required", nameof(identityDocumentType));

        var existingType = await _repository.GetByIdAsync(identityDocumentType.Id);
        if (existingType == null)
            throw new ArgumentException($"Identity Document Type with ID {identityDocumentType.Id} not found", nameof(identityDocumentType));

        return await _repository.UpdateAsync(identityDocumentType);
    }

    public async Task<bool> DeleteIdentityDocumentTypeAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}