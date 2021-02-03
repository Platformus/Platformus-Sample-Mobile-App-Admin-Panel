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
  public class ProductsController : Platformus.Core.Frontend.Controllers.ControllerBase
  {
    public ProductsController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetAsync([FromQuery]ProductFilter filter, string sorting = null, int? offset = null, int? limit = null, string fields = null)
    {
      return (await this.Storage.GetRepository<int, Data.Entities.Product, ProductFilter>().GetAllAsync(
        filter, sorting, offset, limit, InclusionParser<Data.Entities.Product>.Parse(fields)
      )).Select(p => new Product(p));
    }
  }
}