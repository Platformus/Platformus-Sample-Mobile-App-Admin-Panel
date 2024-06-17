// Copyright © 2023 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using WebApplication.Filters;

namespace WebApplication.Domain.Models
{
  public class Product : IModel<Data.Entities.Product, ProductFilter>
  {
    public int Id { get; set; }
    public Category Category { get; set; }
    public string Code { get; set; }
    public Dictionary Name { get; set; }
    public Dictionary Description { get; set; }
    public decimal Price { get; set; }

    public Data.Entities.Product ToEntity() => new()
    {
      Id = this.Id,
      CategoryId = this.Category?.Id ?? 0,
      Code = this.Code,
      NameId = this.Name?.Id ?? 0,
      DescriptionId = this.Description?.Id ?? 0,
      Price = this.Price
    };
  }
}