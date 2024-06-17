// Copyright © 2023 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace WebApplication.Domain.Models
{
  public class Culture : IModel<Platformus.Core.Data.Entities.Culture, CultureFilter>
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsNeutral { get; set; }
    public bool IsFrontendDefault { get; set; }
    public bool IsBackendDefault { get; set; }

    public Platformus.Core.Data.Entities.Culture ToEntity() => new()
    {
      Id = this.Id,
      Name = this.Name,
      IsNeutral = this.IsNeutral,
      IsFrontendDefault = this.IsFrontendDefault,
      IsBackendDefault = this.IsBackendDefault
    };
  }
}