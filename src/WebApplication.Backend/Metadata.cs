// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.Metadata;

namespace WebApplication.Backend.Metadata
{
  public class Metadata : MetadataBase
  {
    public override IEnumerable<MenuGroup> GetMenuGroups(HttpContext httpContext)
    {
      IStringLocalizer<Metadata> localizer = httpContext.RequestServices.GetService<IStringLocalizer<Metadata>>();

      return new MenuGroup[]
      {
        new MenuGroup(
          localizer["WebApplication"],
          1500,
          new MenuItem[]
          {
            new MenuItem(null, "/backend/categories", localizer["Categories"], 1000, new string[] { Permissions.ManageProducts }),
            new MenuItem(null, "/backend/products", localizer["Products"], 2000, new string[] { Permissions.ManageProducts })
          }
        )
      };
    }
  }
}