﻿namespace ApiInventory.Models;

public class ProductModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }
}
