using ApiInventory.Models;
using ApiInventory.Services.Interfaces;

namespace ApiInventory.Services;

public class StockService : IStockService
{
    private List<ProductModel> _products;

    private readonly IDataProvider _dataProvider;

    public decimal CommonCost { get; private set; }

    public StockService(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;

        _products = _dataProvider.Load();

        if (_products == null)
        {
            _products = new List<ProductModel>(0);
        }

        ComputeCommonCost();
    }

    private void ComputeCommonCost()
    {
        CommonCost = 0;

        foreach (var product in _products)
        {
            CommonCost += product.Price * product.Count;
        }
    }

    private bool Contains(int code)
    {
        foreach (var product in _products)
        {
            if (product.Code == code) return true;
        }

        return false;
    }

    public ResultModel Remove(ProductActionDto productActionDto)
    {
        ProductModel? product = GetProduct(productActionDto);

        if (product == null)
        {
            return new ResultModel { Success = false, Message = "There is no such product" };
        }

        _products.Remove(product);

        ComputeCommonCost();

        _dataProvider.Remove(product);

        return new ResultModel { Success = true, Message = "Ok" };
    }

    public ResultModel Add(AddProductDto product)
    {
        if (Contains(product.Code))
        {
            return new ResultModel { Success = false, Message = "This product already exists" };
        }

        var productModel = new ProductModel
        {
            Code = product.Code,
            Count = product.Count,
            Name = product.Name,
            Price = product.Price
        };

        _products.Add(productModel);

        ComputeCommonCost();

        _dataProvider.Add(productModel);

        return new ResultModel { Success = true, Message = "Ok" };
    }

    public void Clear()
    {
        _products.Clear();

        CommonCost = 0;

        _dataProvider.Clear();
    }

    public ProductModel? GetProduct(ProductActionDto productDto)
    {
        foreach (var product in _products)
        {
            if (product.Id == productDto.Id) return product;
        }

        return null;
    }

    public ProductListDto GetProductList()
    {
        return new ProductListDto 
            { 
                products = new List<ProductModel>(_products), 
                Count = _products.Count(),
                CommonCost = CommonCost
            };
    }

    public ResultModel AddProductCount(ChangeProductCntDto productDto)
    {
        if (productDto.Count < 0)
        {
            return new ResultModel { Success = false, Message = "Count has a wrong value" };
        }

        ProductModel? product = GetProduct(new ProductActionDto { Id = productDto.Id });

        if (product == null)
        {
            return new ResultModel { Success = false, Message = "There is no such product" };
        }

        product.Count += productDto.Count;

        ComputeCommonCost();

        _dataProvider.Update(product);

        return new ResultModel { Success = true, Message = "Ok" };
    }

    public ResultModel RemoveProductCount(ChangeProductCntDto productDto)
    {
        ProductModel? product = GetProduct(new ProductActionDto { Id = productDto.Id });

        if (product == null)
        {
            return new ResultModel { Success = false, Message = "There is no such product" };
        }

        if (productDto.Count <= 0 || product.Count < productDto.Count)
        {
            return new ResultModel { Success = false, Message = "Count has a wrong value" };
        }

        product.Count -= productDto.Count;

        ComputeCommonCost();

        _dataProvider.Update(product);

        return new ResultModel { Success = true, Message = "Ok" };
    }
}
