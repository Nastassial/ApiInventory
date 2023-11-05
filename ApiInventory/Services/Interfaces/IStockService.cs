using ApiInventory.Models;

namespace ApiInventory.Services.Interfaces;

public interface IStockService
{
    public ResultModel Remove(ProductActionDto productActionDto);

    public ResultModel Remove(ProductModel product);

    public ResultModel Add(ProductModel product);

    public void Clear();

    public ProductModel? GetProduct(ProductActionDto getProductDto);

    public ProductListDto GetProductList();

    public ResultModel AddProductCnt(ChangeProductCntDto productDto);

    public ResultModel RemoveProductCnt(ChangeProductCntDto productDto);

    public void Save();
}
