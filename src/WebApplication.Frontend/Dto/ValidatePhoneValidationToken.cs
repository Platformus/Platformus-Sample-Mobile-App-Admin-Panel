﻿// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace WebApplication.Frontend.Dto
{
  public class ValidatePhoneValidationToken
  {
    public Guid Id { get; set; }
    public string Code { get; set; }
  }
}
