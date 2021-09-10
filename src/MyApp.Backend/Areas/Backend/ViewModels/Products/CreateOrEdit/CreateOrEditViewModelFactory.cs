// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using MyApp.Data.Entities;
using MyApp.Filters;
using Platformus;
using Platformus.Core.Primitives;

namespace MyApp.Backend.ViewModels.Products
{
  public static class CreateOrEditViewModelFactory
  {
    public static async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Product product)
    {
      if (product == null)
        return new CreateOrEditViewModel()
        {
          CategoryOptions = await GetCategoryOptionsAsync(httpContext),
          NameLocalizations = httpContext.GetLocalizations(),
          DescriptionLocalizations = httpContext.GetLocalizations()
        };

      return new CreateOrEditViewModel()
      {
        Id = product.Id,
        CategoryId = product.CategoryId,
        CategoryOptions = await GetCategoryOptionsAsync(httpContext),
        Code = product.Code,
        NameLocalizations = httpContext.GetLocalizations(product.Name),
        DescriptionLocalizations = httpContext.GetLocalizations(product.Description),
        Price = product.Price
      };
    }

    private static async Task<IEnumerable<Option>> GetCategoryOptionsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, Category, CategoryFilter>().GetAllAsync(inclusions: new Inclusion<Category>(c => c.Name.Localizations))).Select(
        c => new Option(c.Name.GetLocalizationValue(), c.Id.ToString())
      );
    }
  }
}