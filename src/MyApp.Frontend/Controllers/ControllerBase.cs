using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MyApp.Frontend.Controllers
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public abstract class ControllerBase : Platformus.Core.Frontend.Controllers.ControllerBase
  {
    public ControllerBase(IStorage storage)
      : base(storage)
    {
    }
  }
}