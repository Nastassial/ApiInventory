namespace ApiInventory.Models;

public class AddProductDto
{
    public int Code { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }
}
