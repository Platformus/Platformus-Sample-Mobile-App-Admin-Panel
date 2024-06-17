// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using WebApplication.Data.Entities;

namespace WebApplication.Backend.ViewModels.Categories
{
  public static class CreateOrEditViewModelMapper
  {
    public static Category Map(Category category, CreateOrEditViewModel createOrEdit)
    {
      category.Position = createOrEdit.Position;
      return category;
    }
  }
}