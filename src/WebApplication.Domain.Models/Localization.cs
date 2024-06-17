// Copyright © 2023 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace WebApplication.Domain.Models
{
  public class Localization : IModel<Platformus.Core.Data.Entities.Localization, LocalizationFilter>
  {
    public int Id { get; set; }
    public Dictionary Dictionary { get; set; }
    public Culture Culture { get; set; }
    public string Value { get; set; }

    public Platformus.Core.Data.Entities.Localization ToEntity() => new()
    {
      Id = this.Id,
      DictionaryId = this.Dictionary?.Id ?? 0,
      CultureId = this.Culture?.Id,
      Value = this.Value
    };
  }
}