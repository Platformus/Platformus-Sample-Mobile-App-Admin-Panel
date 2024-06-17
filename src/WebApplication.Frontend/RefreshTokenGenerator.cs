// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Security.Cryptography;

namespace WebApplication.Frontend
{
  public class RefreshTokenGenerator
  {
    public string Generate()
    {
      byte[] randomNumber = new byte[32];

      using (var rng = RandomNumberGenerator.Create())
      {
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
      }
    }
  }
}
