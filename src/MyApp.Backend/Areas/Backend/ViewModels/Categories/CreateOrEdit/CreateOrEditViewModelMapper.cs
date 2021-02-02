// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using MyApp.Data.Entities;
using Platformus.Core.Backend.ViewModels;

namespace MyApp.Backend.ViewModels.Categories
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public Category Map(Category category, CreateOrEditViewModel createOrEdit)
    {
      category.Position = createOrEdit.Position;
      return category;
    }
  }
}