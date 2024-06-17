// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus;
using WebApplication.Data.Entities;

namespace WebApplication.Backend.ViewModels.Shared
{
  public static class CategoryViewModelFactory
  {
    public static CategoryViewModel Create(Category category)
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