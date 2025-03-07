using PoC.TestWServ2.Common.Entities;

namespace PoC.TestWServ2.Common.Ports;

public interface ISoldProductsRepository
{
    Task<IEnumerable<SoldProducts>> GetByPersonIdAsync(int personId);
    Task<IEnumerable<SoldProducts>> GetAllAsync();
    Task<SoldProducts> CreateAsync(SoldProducts soldProduct);
}