using ApiInventory.Models;

namespace ApiInventory.Services.Interfaces;

public interface IStockService
{
    ResultModel Remove(ProductActionDto productActionDto);

    ResultModel Add(AddProductDto product);

    void Clear();

    ProductModel? GetProduct(ProductActionDto getProductDto);

    ProductListDto GetProductList();

    ResultModel AddProductCount(ChangeProductCntDto productDto);

    ResultModel RemoveProductCount(ChangeProductCntDto productDto);
}
