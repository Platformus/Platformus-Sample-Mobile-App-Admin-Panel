// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using MyApp.Data.Entities;
using Platformus;
using Platformus.Core.Backend.ViewModels;

namespace MyApp.Backend.ViewModels.Shared
{
  public class ProductViewModelFactory : ViewModelFactoryBase
  {
    public ProductViewModel Create(Product product)
    {
      return new ProductViewModel()
      {
        Id = product.Id,
        Category = new CategoryViewModelFactory().Create(product.Category),
        Name = product.Name.GetLocalizationValue(),
        Price = product.Price
      };
    }
  }
}