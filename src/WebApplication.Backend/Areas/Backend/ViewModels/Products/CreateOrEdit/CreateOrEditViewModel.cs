﻿// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Core.Backend;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;

namespace WebApplication.Backend.ViewModels.Products
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }

    [Display(Name = "Category")]
    [Required]
    public int CategoryId { get; set; }
    public IEnumerable<Option> CategoryOptions { get; set; }

    [Display(Name = "Code")]
    [Required]
    [StringLength(32)]
    public string Code { get; set; }

    [Multilingual]
    [Display(Name = "Name")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }
    public IEnumerable<Localization> NameLocalizations { get; set; }

    [Multilingual]
    [Display(Name = "Description")]
    public string Description { get; set; }
    public IEnumerable<Localization> DescriptionLocalizations { get; set; }

    [Display(Name = "Price")]
    [Required]
    public decimal Price { get; set; }
  }
}