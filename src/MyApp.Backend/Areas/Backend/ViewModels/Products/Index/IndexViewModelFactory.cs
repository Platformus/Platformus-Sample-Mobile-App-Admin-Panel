// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MyApp.Backend.ViewModels.Shared;
using MyApp.Data.Entities;
using MyApp.Filters;
using Platformus;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;

namespace MyApp.Backend.ViewModels.Products
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public async Task<IndexViewModel> CreateAsync(HttpContext httpContext, ProductFilter filter, IEnumerable<Product> products, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.RequestServices.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext,
          new[] {
            new FilterViewModelFactory().Create(httpContext, "Category.Id", localizer["Category"], await this.GetCategoryOptionsAsync(httpContext)),
            new FilterViewModelFactory().Create(httpContext, "Name.Value.Contains", localizer["Name"])
          },
          orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Category"]),
            new GridColumnViewModelFactory().Create(localizer["Name"], httpContext.CreateLocalizedOrderBy("Name")),
            new GridColumnViewModelFactory().Create(localizer["Price"], "Price"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          products.Select(p => new ProductViewModelFactory().Create(p)),
          "_Product"
        )
      };
    }

    private async Task<IEnumerable<Option>> GetCategoryOptionsAsync(HttpContext httpContext)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();
      List<Option> options = new List<Option>();

      options.Add(new Option(localizer["All categories"], string.Empty));
      options.AddRange(
        (await httpContext.GetStorage().GetRepository<int, Category, CategoryFilter>().GetAllAsync(inclusions: new Inclusion<Category>(c => c.Name.Localizations))).Select(
          c => new Option(c.Name.GetLocalizationValue(), c.Id.ToString())
        )
      );

      return options;
    }
  }
}