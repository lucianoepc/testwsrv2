using PoC.TestWServ2.Common.Entities;

namespace PoC.TestWSrv2.BusinessLogic;

public interface ISoldProductsService
{
    Task<IEnumerable<SoldProducts>> GetSoldProductsByPersonIdAsync(int personId);
    Task<IEnumerable<SoldProducts>> GetAllSoldProductsAsync();
    Task<SoldProducts> CreateSoldProductAsync(SoldProducts soldProduct);
}