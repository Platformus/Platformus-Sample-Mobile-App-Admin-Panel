// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Frontend.Controllers
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [ApiController]
  public abstract class ControllerBase : Controller
  {
    protected void SetPagingHeaders(int totalNumber, int? offset, int? limit)
    {
      this.Response.Headers["Paging-Total-Number"] = totalNumber.ToString();

      if (offset != null)
        this.Response.Headers["Paging-Offset"] = offset.ToString();

      if (limit != null)
        this.Response.Headers["Paging-Limit"] = limit.ToString();
    }

    protected string[] GetInclusions()
    {
      return this.Request.Query["fields"].ToString()?.Split(',');
    }
  }
}