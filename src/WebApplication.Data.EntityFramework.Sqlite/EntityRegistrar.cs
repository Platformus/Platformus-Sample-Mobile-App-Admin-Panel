// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data.Entities;

namespace WebApplication.Data.EntityFramework.Sqlite
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<PhoneValidationToken>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Phone).IsRequired().HasMaxLength(16);
          etb.ToTable("PhoneValidationTokens");
        }
      );

      modelBuilder.Entity<RefreshToken>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).IsRequired().HasMaxLength(64);
          etb.ToTable("RefreshTokens");
        }
      );

      modelBuilder.Entity<Category>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ToTable("Categories");
        }
      );

      modelBuilder.Entity<Product>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Price).HasConversion<double>();
          etb.ToTable("Products");
        }
      );
    }
  }
}