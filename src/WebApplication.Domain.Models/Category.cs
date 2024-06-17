// Copyright © 2023 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using WebApplication.Filters;

namespace WebApplication.Domain.Models
{
  public class Category : IModel<Data.Entities.Category, CategoryFilter>
  {
    public int Id { get; set; }
    public Dictionary Name { get; set; }
    public int? Position { get; set; }

    public Data.Entities.Category ToEntity() => new()
    {
      Id = this.Id,
      NameId = this.Name?.Id ?? 0,
      Position = this.Position ?? 0
    };
  }
}