// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MyApp.Filters;
using MyApp.Frontend.Dto;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using Platformus.Core.Services.Abstractions;

namespace MyApp.Frontend.Controllers
{
  [Route("v1/access-tokens")]
  [ApiController]
  public class AccessTokensController : Platformus.Core.Frontend.Controllers.ControllerBase
  {
    private IUserManager userManager;
    private AccessTokenGenerator accessTokenGenerator;
    private RefreshTokenGenerator refreshTokenGenerator;

    private IRepository<Guid, Data.Entities.PhoneValidationToken, PhoneValidationTokenFilter> PhoneValidationTokenRepository
    {
      get => this.Storage.GetRepository<Guid, Data.Entities.PhoneValidationToken, PhoneValidationTokenFilter>();
    }

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
    public async Task<ActionResult<string>> PostAsync([FromBody] CreateAccessToken createAccessToken)
    {
      if (!string.IsNullOrEmpty(createAccessToken.Username) && !string.IsNullOrEmpty(createAccessToken.Password))
        return await this.GetAccessTokenByUsernameAndPasswordAsync(createAccessToken.Username, createAccessToken.Password);

      if (createAccessToken.PhoneValidationTokenId != null)
        return await this.GetAccessTokenByPhoneValidationTokenAsync((Guid)createAccessToken.PhoneValidationTokenId);

      return await this.GetAccessTokenByRefreshTokenAsync(createAccessToken.RefreshToken);
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

    private async Task<ActionResult<string>> GetAccessTokenByPhoneValidationTokenAsync(Guid phoneValidationTokenId)
    {
      Data.Entities.PhoneValidationToken _phoneValidationToken = await this.PhoneValidationTokenRepository.GetByIdAsync(phoneValidationTokenId);

      if (_phoneValidationToken == null)
        return this.BadRequest(new Error("Phone validation token is invalid.", code: "AccessTokens002"));

      if (_phoneValidationToken.Validated == null)
        return this.BadRequest(new Error("Phone validation token is not validated.", code: "AccessTokens003"));

      if (_phoneValidationToken.Validated < DateTime.Now.AddMinutes(3) || _phoneValidationToken.Used != null)
        return this.BadRequest(new Error("Phone validation token expired.", code: "AccessTokens004"));

      _phoneValidationToken.Used = DateTime.Now;
      this.PhoneValidationTokenRepository.Edit(_phoneValidationToken);

      User _user = (await this.UserRepository.GetAllAsync(
        new UserFilter() { Credential = new CredentialFilter() { CredentialType = new CredentialTypeFilter() { Code = "phone" }, Identifier = new StringFilter(equals: _phoneValidationToken.Phone) } },
        inclusions: new Inclusion<User>("UserRoles.Role.RolePermissions.Permission")
      )).FirstOrDefault();

      if (_user == null)
        return this.BadRequest();

      Data.Entities.RefreshToken _refreshToken = this.CreateRefreshToken(_user);

      this.RefreshTokenRepository.Create(_refreshToken);
      await this.Storage.SaveAsync();
      this.Response.Headers["Refresh-Token"] = _refreshToken.Id;
      return this.accessTokenGenerator.Generate(_user);
    }

    private async Task<ActionResult<string>> GetAccessTokenByRefreshTokenAsync(string refreshToken)
    {
      Data.Entities.RefreshToken _refreshToken = await this.RefreshTokenRepository.GetByIdAsync(
        refreshToken,
        new Inclusion<Data.Entities.RefreshToken>("User.UserRoles.Role.RolePermissions.Permission")
      );

      if (_refreshToken == null)
        return this.BadRequest(new Error("Refresh token is invalid.", code: "AccessTokens005"));

      if (_refreshToken.Created < DateTime.Now.AddMonths(-1) || _refreshToken.Used != null)
        return this.BadRequest(new Error("Refresh token expired.", code: "AccessTokens006"));

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