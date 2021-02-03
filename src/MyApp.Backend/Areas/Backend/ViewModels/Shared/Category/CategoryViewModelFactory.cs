﻿// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using MyApp.Data.Entities;
using Platformus;
using Platformus.Core.Backend.ViewModels;

namespace MyApp.Backend.ViewModels.Shared
{
  public class CategoryViewModelFactory : ViewModelFactoryBase
  {
    public CategoryViewModel Create(Category category)
    {
      return new CategoryViewModel()
      {
        Id = category.Id,
        Name = category.Name.GetLocalizationValue(),
        Position = category.Position
      };
    }
  }
}