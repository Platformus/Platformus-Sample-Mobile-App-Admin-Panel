// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
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
  [Route("{culture}/v1/products")]
  public class ProductsController : ControllerBase
  {
    private IRepository<int, Data.Entities.Product, ProductFilter> Repository
    {
      get => this.Storage.GetRepository<int, Data.Entities.Product, ProductFilter>();
    }

    public ProductsController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetAsync([FromQuery]ProductFilter filter, string sorting = null, int? offset = null, int? limit = null, string fields = null)
    {
      this.SetPagingHeaders(await this.Repository.CountAsync(filter), offset, limit);
      return (await this.Repository.GetAllAsync(
        filter, sorting, offset, limit, InclusionParser<Data.Entities.Product>.Parse(fields)
      )).Select(p => new Product(p));
    }
  }
}