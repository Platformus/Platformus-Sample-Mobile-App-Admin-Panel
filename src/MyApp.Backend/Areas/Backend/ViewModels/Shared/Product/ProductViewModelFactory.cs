// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using MyApp.Data.Entities;
using Platformus;

namespace MyApp.Backend.ViewModels.Shared
{
  public static class ProductViewModelFactory
  {
    public static ProductViewModel Create(Product product)
    {
      return new ProductViewModel()
      {
        Id = product.Id,
        Category = CategoryViewModelFactory.Create(product.Category),
        Name = product.Name.GetLocalizationValue(),
        Price = product.Price
      };
    }
  }
}