// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace MyApp.Frontend.Dto
{
  public class CreatePhoneValidationToken
  {
    public string Phone { get; set; }
    public bool? UserExists { get; set; }

    public Data.Entities.PhoneValidationToken ToEntity()
    {
      return new Data.Entities.PhoneValidationToken()
      {
        Phone = this.Phone
      };
    }
  }
}
