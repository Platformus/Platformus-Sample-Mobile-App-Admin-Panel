// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace WebApplication.Filters
{
  public class RefreshTokenFilter : IFilter
  {
    public StringFilter Id { get; set; }
    public DateTimeFilter Created { get; set; }
    public DateTimeFilter Used { get; set; }
  }
}