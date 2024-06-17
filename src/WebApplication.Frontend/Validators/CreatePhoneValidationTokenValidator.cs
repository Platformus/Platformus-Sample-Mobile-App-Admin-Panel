// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using WebApplication.Frontend.Dto;

namespace WebApplication.Frontend.Validators
{
  public class CreatePhoneValidationTokenValidator : AbstractValidator<CreatePhoneValidationToken>
  {
    public CreatePhoneValidationTokenValidator()
    {
      this.RuleFor(m => m.Phone).NotEmpty().MaximumLength(16);
    }
  }
}