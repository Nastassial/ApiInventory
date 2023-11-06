using ApiInventory.Models;
using ApiInventory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiInventory.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    public ProductListDto GetProductList()
    {
        return _stockService.GetProductList();
    }

    [HttpPost]
    public ProductModel? GetProduct([FromBody] ProductActionDto product)
    {
        return _stockService.GetProduct(product);
    }

    [HttpPost]
    public IActionResult AddProduct([FromBody] ProductModel product)
    {
        ResultModel result = _stockService.Add(product);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete]
    public void ClearStock()
    {
        _stockService.Clear();
    }

    [HttpPost]
    public IActionResult DeleteProduct([FromBody] ProductActionDto product)
    {
        ResultModel result = _stockService.Remove(product);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddProductCnt([FromBody] ChangeProductCntDto product)
    {
        ResultModel result = _stockService.AddProductCnt(product);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    public IActionResult RemoveProductCnt([FromBody] ChangeProductCntDto product)
    {
        ResultModel result = _stockService.RemoveProductCnt(product);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
