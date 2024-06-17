// Copyright © 2023 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Magicalizer.Domain.Models.Abstractions;
using WebApplication.Filters;

namespace WebApplication.Domain.Models
{
  public class PhoneValidationToken : IModel<Data.Entities.PhoneValidationToken, PhoneValidationTokenFilter>
  {
    public Guid Id { get; set; }
    public string Phone { get; set; }
    public string Code { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Validated { get; set; }
    public DateTime? Used { get; set; }

    public Data.Entities.PhoneValidationToken ToEntity() => new()
    {
      Id = this.Id,
      Phone = this.Phone,
      Code = this.Code,
      Created = this.Created,
      Validated = this.Validated,
      Used = this.Used
    };
  }
}