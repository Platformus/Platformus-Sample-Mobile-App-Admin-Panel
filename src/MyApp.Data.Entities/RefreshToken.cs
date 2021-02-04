// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Magicalizer.Data.Entities.Abstractions;
using Platformus.Core.Data.Entities;

namespace MyApp.Data.Entities
{
  public class RefreshToken : IEntity
  {
    public string Id { get; set; }
    public int UserId { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Used { get; set; }

    public virtual User User { get; set; }
  }
}