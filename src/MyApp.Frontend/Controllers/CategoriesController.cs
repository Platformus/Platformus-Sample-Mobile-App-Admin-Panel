﻿// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MyApp.Filters;
using MyApp.Frontend.Dto;

namespace MyApp.Frontend.Controllers
{
  [Route("v1/categories")]
  public class CategoriesController : ControllerBase
  {
    private IRepository<int, Data.Entities.Category, CategoryFilter> Repository
    {
      get => this.Storage.GetRepository<int, Data.Entities.Category, CategoryFilter>();
    }

    public CategoriesController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    public async Task<IEnumerable<Category>> GetAsync([FromQuery] CategoryFilter filter, string sorting = null, int? offset = null, int? limit = null, string fields = null)
    {
      this.SetPagingHeaders(await this.Repository.CountAsync(filter), offset, limit);
      return (await this.Repository.GetAllAsync(
        filter, sorting, offset, limit, InclusionParser<Data.Entities.Category>.Parse(fields)
      )).Select(c => new Category(c));
    }
  }
}