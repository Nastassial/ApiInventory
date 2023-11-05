namespace ApiInventory.Models;

public class ProductListDto
{
    public ProductModel[] products { get; set; }

    public int Count => products.Count();
}
