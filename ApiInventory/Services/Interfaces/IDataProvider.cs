using ApiInventory.Models;

namespace ApiInventory.Services.Interfaces;

public interface IDataProvider
{
    void Save(List<ProductModel> productList);

    List<ProductModel> Load();

    void Clear();
}
