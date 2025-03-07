using PoC.TestWServ2.Common.Entities;
using PoC.TestWServ2.Common.Ports;

namespace PoC.TestWSrv2.BusinessLogic;

public class SoldProductsService : ISoldProductsService
{
    private readonly ISoldProductsRepository _repository;
    private readonly IPersonRepository _personRepository;
    private readonly IProductRepository _productRepository;

    public SoldProductsService(
        ISoldProductsRepository repository,
        IPersonRepository personRepository,
        IProductRepository productRepository)
    {
        _repository = repository;
        _personRepository = personRepository;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<SoldProducts>> GetSoldProductsByPersonIdAsync(int personId)
    {
        var person = await _personRepository.GetByIdAsync(personId);
        if (person == null)
            throw new ArgumentException($"Person with ID {personId} not found", nameof(personId));

        return await _repository.GetByPersonIdAsync(personId);
    }

    public async Task<IEnumerable<SoldProducts>> GetAllSoldProductsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<SoldProducts> CreateSoldProductAsync(SoldProducts soldProduct)
    {
        var person = await _personRepository.GetByIdAsync(soldProduct.PersonId);
        if (person == null)
            throw new ArgumentException($"Person with ID {soldProduct.PersonId} not found", nameof(soldProduct));

        var product = await _productRepository.GetByIdAsync(soldProduct.ProductId);
        if (product == null)
            throw new ArgumentException($"Product with ID {soldProduct.ProductId} not found", nameof(soldProduct));

        if (soldProduct.Quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(soldProduct));

        soldProduct.PersonCode = person.Code;
        soldProduct.ProductCode = product.Code;
        
        if (soldProduct.SaleDate == default)
            soldProduct.SaleDate = DateTime.UtcNow;

        return await _repository.CreateAsync(soldProduct);
    }
}