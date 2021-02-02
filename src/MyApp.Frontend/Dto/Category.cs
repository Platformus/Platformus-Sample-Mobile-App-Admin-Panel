﻿// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus;

namespace MyApp.Frontend.Dto
{
  public class Category
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }

    public Category() { }

    public Category(HttpContext httpContext, Data.Entities.Category _category)
    {
      this.Id = _category.Id;
      this.Name = _category.Name?.GetLocalizationValue(httpContext);
      this.Position = _category.Position;
    }
  }
}
