// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using MyApp.Backend.ViewModels.Shared;
using MyApp.Data.Entities;

namespace MyApp.Backend.ViewModels.Categories
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(string sorting, int offset, int limit, int total, IEnumerable<Category> categories)
    {
      return new IndexViewModel()
      {
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total,
        Categories = categories.Select(CategoryViewModelFactory.Create)
      };
    }
  }
}