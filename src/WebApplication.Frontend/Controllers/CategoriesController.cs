// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Domain.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Filters;
using WebApplication.Frontend.Dto;

namespace WebApplication.Frontend.Controllers
{
  [Route("v1/categories")]
  public class CategoriesController : ControllerBase
  {
    private readonly IService<int, Domain.Models.Category, CategoryFilter> service;

    public CategoriesController(IServiceResolver serviceResolver)
    {
      this.service = serviceResolver.GetService<int, Domain.Models.Category, CategoryFilter>();
    }

    [HttpGet]
    public async Task<IEnumerable<Category>> GetAsync([FromQuery] CategoryFilter filter, string sorting = null, int? offset = null, int? limit = null, string fields = null)
    {
      this.SetPagingHeaders(await this.service.CountAsync(filter), offset, limit);
      return (await this.service.GetAllAsync(
        filter, sorting, offset, limit, this.GetInclusions()
      )).Select(c => new Category(c));
    }
  }
}