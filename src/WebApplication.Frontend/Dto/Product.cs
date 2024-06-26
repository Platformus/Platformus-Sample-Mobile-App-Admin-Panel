﻿// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus;
using WebApplication.Frontend.Extensions;

namespace WebApplication.Frontend.Dto
{
  public class Product
  {
    public int Id { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Product() { }

    public Product(Domain.Models.Product _product)
    {
      this.Id = _product.Id;
      this.Category = _product.Category == null ? null : new Category(_product.Category);
      this.Name = _product.Name?.GetLocalizationValue();
      this.Description = _product.Description?.GetLocalizationValue();
      this.Price = _product.Price;
    }
  }
}
