// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using MyApp.Data.Entities;
using Platformus;

namespace MyApp.Backend.ViewModels.Categories
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(HttpContext httpContext, Category category)
    {
      if (category == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = httpContext.GetLocalizations()
        };

      return new CreateOrEditViewModel()
      {
        Id = category.Id,
        NameLocalizations = httpContext.GetLocalizations(category.Name),
        Position = category.Position
      };
    }
  }
}