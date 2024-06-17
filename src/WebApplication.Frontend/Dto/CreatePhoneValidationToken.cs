// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace WebApplication.Frontend.Dto
{
  public class CreatePhoneValidationToken
  {
    public string Phone { get; set; }
    public bool? UserExists { get; set; }

    public Domain.Models.PhoneValidationToken ToModel()
    {
      return new Domain.Models.PhoneValidationToken()
      {
        Phone = this.Phone
      };
    }
  }
}
