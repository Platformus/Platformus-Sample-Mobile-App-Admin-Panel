// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MyApp.Backend.ViewModels.Shared;
using MyApp.Data.Entities;
using MyApp.Filters;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;

namespace MyApp.Backend.ViewModels.Categories
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, CategoryFilter filter, IEnumerable<Category> categories, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.RequestServices.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext, "Name.Value.Contains", orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Name"], httpContext.CreateLocalizedOrderBy("Name")),
            new GridColumnViewModelFactory().Create(localizer["Position"], "Position"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          categories.Select(c => new CategoryViewModelFactory().Create(c)),
          "_Category"
        )
      };
    }
  }
}