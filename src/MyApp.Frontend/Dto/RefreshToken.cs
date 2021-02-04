﻿// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace MyApp.Frontend.Dto
{
  public class RefreshToken
  {
    public string Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Used { get; set; }
  }
}