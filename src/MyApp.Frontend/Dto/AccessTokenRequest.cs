// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace MyApp.Frontend.Dto
{
  public class AccessTokenRequest
  {
    public string Username { get; set; }
    public string Password { get; set; }
    public string RefreshToken { get; set; }
  }
}