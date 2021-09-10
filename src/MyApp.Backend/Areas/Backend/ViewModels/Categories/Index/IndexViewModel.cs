// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using MyApp.Backend.ViewModels.Shared;
using Platformus.Core.Backend.ViewModels;

namespace MyApp.Backend.ViewModels.Categories
{
  public class IndexViewModel : ViewModelBase
  {
    public string Sorting { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int Total { get; set; }
    public IEnumerable<CategoryViewModel> Categories { get; set; }
  }
}