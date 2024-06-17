// Copyright © 2023 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Data.Entities;
using WebApplication.Filters;

namespace WebApplication.Domain.Models
{
  public class RefreshToken : IModel<Data.Entities.RefreshToken, RefreshTokenFilter>
  {
    public string Id { get; set; }
    public User User { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Used { get; set; }

    public Data.Entities.RefreshToken ToEntity() => new()
    {
      Id = this.Id,
      UserId = this.User?.Id ?? 0,
      Created = this.Created,
      Used = this.Used
    };
  }
}