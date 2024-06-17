// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platformus.WebApplication.Extensions;
using WebApplication.Frontend;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Services.Configure<StorageContextOptions>(options =>
  {
    options.ConnectionString = builder.Configuration.GetConnectionString("Default");
  }
);

builder.Services.Configure<JwtOptions>(options =>
  {
    options.Secret = builder.Configuration["Jwt:Secret"];
  }
);

builder.Services.AddPlatformus();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI(options =>
    {
      options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication API V1");
    }
  );
}

app.UsePlatformus();
app.Run();