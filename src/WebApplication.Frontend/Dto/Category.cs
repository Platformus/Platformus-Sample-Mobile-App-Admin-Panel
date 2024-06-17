// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus;
using WebApplication.Frontend.Extensions;

namespace WebApplication.Frontend.Dto
{
  public class Category
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }

    public Category() { }

    public Category(Domain.Models.Category _category)
    {
      this.Id = _category.Id;
      this.Name = _category.Name?.GetLocalizationValue();
      this.Position = _category.Position;
    }
  }
}
