using System;
using System.Security.Cryptography;

namespace MyApp
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
