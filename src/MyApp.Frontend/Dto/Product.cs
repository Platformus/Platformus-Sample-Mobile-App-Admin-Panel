// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus;

namespace MyApp.Frontend.Dto
{
  public class Product
  {
    public int Id { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Product() { }

    public Product(HttpContext httpContext, Data.Entities.Product _product)
    {
      this.Id = _product.Id;
      this.Category = _product.Category == null ? null : new Category(httpContext, _product.Category);
      this.Name = _product.Name?.GetLocalizationValue(httpContext);
      this.Description = _product.Description?.GetLocalizationValue(httpContext);
      this.Price = _product.Price;
    }
  }
}
