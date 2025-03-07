using PoC.TestWServ2.Common.Entities;
using PoC.TestWServ2.Common.Ports;

namespace PoC.TestWSrv2.BusinessLogic;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Code))
            throw new ArgumentException("Code is required", nameof(product));

        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Name is required", nameof(product));

        if (product.BaseCost < 0)
            throw new ArgumentException("Base cost cannot be negative", nameof(product));

        return await _repository.CreateAsync(product);
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Code))
            throw new ArgumentException("Code is required", nameof(product));

        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Name is required", nameof(product));

        if (product.BaseCost < 0)
            throw new ArgumentException("Base cost cannot be negative", nameof(product));

        var existingProduct = await _repository.GetByIdAsync(product.Id);
        if (existingProduct == null)
            throw new ArgumentException($"Product with ID {product.Id} not found", nameof(product));

        return await _repository.UpdateAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}