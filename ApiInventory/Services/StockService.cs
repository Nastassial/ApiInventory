﻿using ApiInventory.Models;
using ApiInventory.Services.Interfaces;

namespace ApiInventory.Services;

public class StockService : IStockService
{
    private List<ProductModel> _products;

    private readonly IDataProvider _dataProvider;

    public int ProductTypesCount => _products.Count;

    public decimal CommonCost { get; private set; }

    public StockService()
    {
        _products = new List<ProductModel>(0);

        CommonCost = 0;
    }

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

    private bool Contains(int id)
    {
        foreach (var product in _products)
        {
            if (product.Id == id) return true;
        }

        return false;
    }

    private bool Contains(ProductModel product)
    {
        return _products.Contains(product);
    }

    public ResultModel Remove(ProductActionDto productActionDto)
    {
        ProductModel? product = GetProduct(productActionDto);

        if (product == null)
        {
            return new ResultModel { Success = false, Message = "There is no such product" };
        }

        Remove(product);

        ComputeCommonCost();

        return new ResultModel { Success = true, Message = "Ok" };
    }

    public ResultModel Remove(ProductModel product)
    {
        if (!Contains(product) && !Contains(product.Id))
        {
            return new ResultModel { Success = false, Message = "There is no such product" };
        }

        _products.Remove(product);

        ComputeCommonCost();

        return new ResultModel { Success = true, Message = "Ok" };
    }

    public ResultModel Add(ProductModel product)
    {
        if (Contains(product) || Contains(product.Id))
        {
            return new ResultModel { Success = false, Message = "This product already exists" };
        }

        _products.Add(product);

        ComputeCommonCost();

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
        return new ProductListDto { products = _products.ToArray() };
    }

    public ResultModel AddProductCnt(ChangeProductCntDto productDto)
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

        return new ResultModel { Success = true, Message = "Ok" };
    }

    public ResultModel RemoveProductCnt(ChangeProductCntDto productDto)
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

        return new ResultModel { Success = true, Message = "Ok" };
    }

    public void Save()
    {
        _dataProvider.Save(_products);
    }
}