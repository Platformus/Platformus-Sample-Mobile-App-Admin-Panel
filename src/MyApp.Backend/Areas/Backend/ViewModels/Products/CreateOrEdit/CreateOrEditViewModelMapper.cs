﻿// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using MyApp.Data.Entities;
using Platformus.Core.Backend.ViewModels;

namespace MyApp.Backend.ViewModels.Products
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public Product Map(Product product, CreateOrEditViewModel createOrEdit)
    {
      product.CategoryId = createOrEdit.CategoryId;
      product.Code = createOrEdit.Code;
      product.Price = createOrEdit.Price;
      return product;
    }
  }
}