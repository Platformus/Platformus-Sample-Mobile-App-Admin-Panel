using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MyApp.Filters;
using MyApp.Frontend.Dto;

namespace MyApp.Frontend.Controllers
{
  [Route("{culture}/v1/categories")]
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
      return (await this.Repository.GetAllAsync(
        filter, sorting, offset, limit, InclusionParser<Data.Entities.Category>.Parse(fields)
      )).Select(c => new Category(c));
    }
  }
}