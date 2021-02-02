// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using MyApp.Data.Entities;
using Platformus;
using Platformus.Core.Backend.ViewModels;

namespace MyApp.Backend.ViewModels.Shared
{
  public class ProductViewModelFactory : ViewModelFactoryBase
  {
    public ProductViewModel Create(HttpContext httpContext, Product product)
    {
      return new ProductViewModel()
      {
        Id = product.Id,
        Category = new CategoryViewModelFactory().Create(httpContext, product.Category),
        Name = product.Name.GetLocalizationValue(httpContext),
        Price = product.Price
      };
    }
  }
}