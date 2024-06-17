// Copyright © 2023 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace WebApplication.Domain.Models
{
  public class Dictionary : IModel<Platformus.Core.Data.Entities.Dictionary, DictionaryFilter>
  {
    public int Id { get; set; }
    public IEnumerable<Localization> Localizations { get; set; }

    public Platformus.Core.Data.Entities.Dictionary ToEntity() => new()
    {
      Id = this.Id,
      Localizations = this.Localizations?.Select(l => l.ToEntity()).ToList()
    };
  }
}