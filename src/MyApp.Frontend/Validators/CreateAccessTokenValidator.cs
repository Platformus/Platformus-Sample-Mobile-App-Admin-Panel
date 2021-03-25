// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using MyApp.Frontend.Dto;

namespace MyApp.Frontend.Validators
{
  public class CreateAccessTokenValidator : AbstractValidator<CreateAccessToken>
  {
    public CreateAccessTokenValidator()
    {
      this.When(m => string.IsNullOrEmpty(m.Username) && string.IsNullOrEmpty(m.Password) && m.PhoneValidationTokenId == null, () => {
        this.RuleFor(m => m.RefreshToken).NotEmpty().MaximumLength(512);
      });

      this.When(m => string.IsNullOrEmpty(m.Username) && string.IsNullOrEmpty(m.Password) && string.IsNullOrEmpty(m.RefreshToken), () => {
        this.RuleFor(m => m.PhoneValidationTokenId).NotEmpty();
      });

      this.When(m => m.PhoneValidationTokenId == null && string.IsNullOrEmpty(m.RefreshToken), () => {
        this.RuleFor(m => m.Username).NotEmpty().MaximumLength(512);
        this.RuleFor(m => m.Password).NotEmpty().MaximumLength(512);
      });

    }
  }
}