// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace WebApplication.Frontend.Dto
{
  public class PhoneValidationToken
  {
    public Guid Id { get; set; }
    public string Phone { get; set; }
    public DateTime Created { get; set; }

    public PhoneValidationToken(Domain.Models.PhoneValidationToken _phoneValidationToken)
    {
      this.Id = _phoneValidationToken.Id;
      this.Phone = _phoneValidationToken.Phone;
      this.Created = _phoneValidationToken.Created;
    }
  }
}