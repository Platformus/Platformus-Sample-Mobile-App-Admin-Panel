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
      return (await this.Repository.GetAllAsync(
        filter, sorting, offset, limit, InclusionParser<Data.Entities.Product>.Parse(fields)
      )).Select(p => new Product(p));
    }
  }
}