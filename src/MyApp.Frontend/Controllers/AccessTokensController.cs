using System;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MyApp.Filters;
using MyApp.Frontend.Dto;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using Platformus.Core.Services.Abstractions;

namespace MyApp.Frontend.Controllers
{
  [Route("{culture}/v1/access-tokens")]
  public class AccessTokensController : Platformus.Core.Frontend.Controllers.ControllerBase
  {
    private IUserManager userManager;
    private AccessTokenGenerator accessTokenGenerator;
    private RefreshTokenGenerator refreshTokenGenerator;

    private IRepository<int, User, UserFilter> UserRepository
    {
      get => this.Storage.GetRepository<int, User, UserFilter>();
    }

    private IRepository<string, Data.Entities.RefreshToken, RefreshTokenFilter> RefreshTokenRepository
    {
      get => this.Storage.GetRepository<string, Data.Entities.RefreshToken, RefreshTokenFilter>();
    }

    public AccessTokensController(IStorage storage, IUserManager userManager, AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator)
      : base(storage)
    {
      this.userManager = userManager;
      this.accessTokenGenerator = accessTokenGenerator;
      this.refreshTokenGenerator = refreshTokenGenerator;
    }

    [HttpPost]
    public async Task<ActionResult<string>> GetAsync([FromBody]AccessTokenRequest accessTokenRequest)
    {
      if (!string.IsNullOrEmpty(accessTokenRequest.Username) && !string.IsNullOrEmpty(accessTokenRequest.Password))
        return await this.GetAccessTokenByUsernameAndPasswordAsync(accessTokenRequest.Username, accessTokenRequest.Password);

      return await this.GetAccessTokenByRefreshTokenAsync(accessTokenRequest.RefreshToken);
    }

    private async Task<ActionResult<string>> GetAccessTokenByUsernameAndPasswordAsync(string username, string password)
    {
      ValidateResult validateResult = await this.userManager.ValidateAsync("Email", username, password);

      if (!validateResult.Success)
        return this.BadRequest(new Error("Username or password is invalid.", code: "AccessTokens001"));

      Data.Entities.RefreshToken _refreshToken = this.CreateRefreshToken(validateResult.User);

      this.RefreshTokenRepository.Create(_refreshToken);
      await this.Storage.SaveAsync();
      this.Response.Headers["Refresh-Token"] = _refreshToken.Id;
      return this.accessTokenGenerator.Generate(
        await this.UserRepository.GetByIdAsync(
          validateResult.User.Id,
          new Inclusion<User>("UserRoles.Role.RolePermissions.Permission")
        )
      );
    }

    private async Task<ActionResult<string>> GetAccessTokenByRefreshTokenAsync(string refreshToken)
    {
      Data.Entities.RefreshToken _refreshToken = await this.RefreshTokenRepository.GetByIdAsync(
        refreshToken,
        new Inclusion<Data.Entities.RefreshToken>("User.UserRoles.Role.RolePermissions.Permission")
      );

      if (_refreshToken == null)
        return this.BadRequest(new Error("Refresh token is invalid.", code: "AccessTokens002"));

      if (_refreshToken.Created < DateTime.Now.AddMonths(-1) || _refreshToken.Used != null)
        return this.BadRequest(new Error("Refresh token expired.", code: "AccessTokens003"));

      _refreshToken.Used = DateTime.Now;
      this.RefreshTokenRepository.Edit(_refreshToken);
      await this.Storage.SaveAsync();

      Data.Entities.RefreshToken _newRefreshToken = this.CreateRefreshToken(_refreshToken.User);

      this.RefreshTokenRepository.Create(_newRefreshToken);
      await this.Storage.SaveAsync();
      this.Response.Headers["Refresh-Token"] = _newRefreshToken.Id;
      return this.accessTokenGenerator.Generate(_refreshToken.User);
    }

    private Data.Entities.RefreshToken CreateRefreshToken(User _user)
    {
      Data.Entities.RefreshToken _refreshToken = new Data.Entities.RefreshToken();

      _refreshToken.Id = this.refreshTokenGenerator.Generate();
      _refreshToken.UserId = _user.Id;
      _refreshToken.Created = DateTime.Now;
      return _refreshToken;
    }
  }
}