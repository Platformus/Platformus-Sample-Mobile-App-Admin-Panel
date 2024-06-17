// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;

namespace WebApplication.Backend.ViewModels.Shared
{
  public class CategoryViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }
  }
}