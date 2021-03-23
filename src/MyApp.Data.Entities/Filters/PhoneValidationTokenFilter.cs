﻿// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Magicalizer.Filters.Abstractions;

namespace MyApp.Filters
{
  public class PhoneValidationTokenFilter : IFilter
  {
    public Guid? Id { get; set; }
    public string Phone { get; set; }
    public DateTimeFilter Created { get; set; }
    public DateTimeFilter Used { get; set; }
  }
}