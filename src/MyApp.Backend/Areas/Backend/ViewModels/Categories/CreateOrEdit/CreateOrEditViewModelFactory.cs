// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyApp.Data.Entities;
using Platformus.Core.Backend.ViewModels;

namespace MyApp.Backend.ViewModels.Categories
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Category category)
    {
      if (category == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = this.GetLocalizations(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = category.Id,
        NameLocalizations = this.GetLocalizations(httpContext, category.Name),
        Position = category.Position
      };
    }
  }
}