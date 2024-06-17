// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;
using Platformus.Core.Data.Entities;

namespace WebApplication.Data.Entities
{
  public class Product : IEntity
  {
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Code { get; set; }
    public int NameId { get; set; }
    public int DescriptionId { get; set; }
    public decimal Price { get; set; }

    public virtual Category Category { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual Dictionary Description { get; set; }
  }
}