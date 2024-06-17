// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Magicalizer.Data.Entities.Abstractions;

namespace WebApplication.Data.Entities
{
  public class PhoneValidationToken : IEntity
  {
    public Guid Id { get; set; }
    public string Phone { get; set; }
    public string Code { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Validated { get; set; }
    public DateTime? Used { get; set; }
  }
}