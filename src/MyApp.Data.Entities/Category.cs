// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;
using Platformus.Core.Data.Entities;

namespace MyApp.Data.Entities
{
  public class Category : IEntity
  {
    public int Id { get; set; }
    public int NameId { get; set; }
    public int? Position { get; set; }

    public virtual Dictionary Name { get; set; }
  }
}