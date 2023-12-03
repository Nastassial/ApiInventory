
using ApiInventory.Database;
using ApiInventory.Models;
using ApiInventory.Services.Interfaces;

namespace ApiInventory.Services;

public class DbDataProvider : IDataProvider
{
    private readonly InventoryDbContext _dbContext;

    public DbDataProvider(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Clear()
    {
        //_dbContext.Database.EnsureDeleted();

        var products = _dbContext.Products.ToList();

        foreach (var product in products)
        {
            _dbContext.Products.Remove(product);
        }

        _dbContext.SaveChanges();
    }

    public void Save(List<ProductModel> productList)
    {
        foreach (var product in productList)
        {
            product.Id = 0;
            _dbContext.Products.Add(product);
        }

        _dbContext.SaveChanges();
    }

    public List<ProductModel> Load()
    {
        List<ProductModel> productModels = new List<ProductModel>();

        var products = _dbContext.Products.ToList();

        foreach (var product in products)
        {
            productModels.Add(product);
        }

        return productModels;
    }

    public void Remove(ProductModel product) 
    {
        _dbContext.Products.Remove(product);

        _dbContext.SaveChanges();
    }

    public void Add(ProductModel product)
    {
        _dbContext.Products.Add(product);

        _dbContext.SaveChanges();
    }

    public void Update(ProductModel product)
    {
        _dbContext.Products.Update(product);

        _dbContext.SaveChanges();
    }
}
