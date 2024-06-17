// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication.Frontend.Actions
{
  public class AddAuthenticationAction : IConfigureServicesAction
  {
    public int Priority => 1001;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      AccessTokenGenerator accessTokenGenerator = serviceProvider.GetService<AccessTokenGenerator>();

      services.AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
      ).AddJwtBearer(o =>
        {
          o.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = accessTokenGenerator.CreateSecurityKey()
          };
        }
      );
    }
  }
}
