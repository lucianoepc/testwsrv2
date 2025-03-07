namespace PoC.TestWServ2.Common.Entities;

public class SoldProducts
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string PersonCode { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public int Quantity { get; set; }

    // Navigation properties
    public Person? Person { get; set; }
    public Product? Product { get; set; }
}