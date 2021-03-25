// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using MyApp.Frontend.Dto;

namespace MyApp.Frontend.Validators
{
  public class ValidatePhoneValidationTokenValidator : AbstractValidator<ValidatePhoneValidationToken>
  {
    public ValidatePhoneValidationTokenValidator()
    {
      this.RuleFor(m => m.Code).NotEmpty().MaximumLength(6);
    }
  }
}