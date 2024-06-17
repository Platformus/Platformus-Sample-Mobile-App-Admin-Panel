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
  [Route("v1/products")]
  public class ProductsController : ControllerBase
  {
    private readonly IService<int, Domain.Models.Product, ProductFilter> service;

    public ProductsController(IServiceResolver serviceResolver)
    {
      this.service = serviceResolver.GetService<int, Domain.Models.Product, ProductFilter>();
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetAsync([FromQuery]ProductFilter filter, string sorting = null, int? offset = null, int? limit = null, string fields = null)
    {
      this.SetPagingHeaders(await this.service.CountAsync(filter), offset, limit);
      return (await this.service.GetAllAsync(
        filter, sorting, offset, limit, this.GetInclusions()
      )).Select(p => new Product(p));
    }
  }
}