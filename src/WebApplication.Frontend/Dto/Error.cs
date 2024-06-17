// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace WebApplication.Frontend.Dto
{
  public class Error
  {
    public string Message { get; set; }
    public string Code { get; set; }

    public Error(string message, string code = null)
    {
      this.Message = message;
      this.Code = code;
    }
  }
}