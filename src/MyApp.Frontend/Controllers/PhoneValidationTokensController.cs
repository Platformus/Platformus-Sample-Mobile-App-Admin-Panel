// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MyApp.Filters;
using MyApp.Frontend.Dto;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;

namespace MyApp.Frontend.Controllers
{
  [Route("v1/phone-validation-tokens")]
  [ApiController]
  public class PhoneValidationTokensController : Platformus.Core.Frontend.Controllers.ControllerBase
  {
    private IRepository<Guid, Data.Entities.PhoneValidationToken, PhoneValidationTokenFilter> PhoneValidationTokenRepository
    {
      get => this.Storage.GetRepository<Guid, Data.Entities.PhoneValidationToken, PhoneValidationTokenFilter>();
    }

    private IRepository<int, Credential, CredentialFilter> CredentialRepository
    {
      get => this.Storage.GetRepository<int, Credential, CredentialFilter>();
    }

    public PhoneValidationTokensController(IStorage storage)
      : base(storage)
    {
    }

    [HttpPost]
    public async Task<ActionResult<PhoneValidationToken>> PostAsync([FromBody] CreatePhoneValidationToken createPhoneVerificationToken)
    {
      if (createPhoneVerificationToken.UserExists == true && await this.CredentialRepository.CountAsync(new CredentialFilter() { CredentialType = new CredentialTypeFilter() { Code = "phone" }, Identifier = new StringFilter(equals: createPhoneVerificationToken.Phone) }) == 0)
        return this.BadRequest(new Error("User does not exist.", code: "PhoneValidationTokens001"));

      if (createPhoneVerificationToken.UserExists == false && await this.CredentialRepository.CountAsync(new CredentialFilter() { CredentialType = new CredentialTypeFilter() { Code = "phone" }, Identifier = new StringFilter(equals: createPhoneVerificationToken.Phone) }) != 0)
        return this.BadRequest(new Error("User already exists.", code: "PhoneValidationTokens002"));

      Data.Entities.PhoneValidationToken _phoneValidationToken = createPhoneVerificationToken.ToEntity();

      _phoneValidationToken.Code = "123456";
      //_phoneValidationToken.Code = new Random().Next(100000, 999999).ToString();
      _phoneValidationToken.Created = DateTime.Now;
      this.PhoneValidationTokenRepository.Create(_phoneValidationToken);
      await this.Storage.SaveAsync();
      return new PhoneValidationToken(_phoneValidationToken);
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync([FromBody] ValidatePhoneValidationToken validatePhoneValidationToken)
    {
      Data.Entities.PhoneValidationToken _phoneValidationToken = await this.PhoneValidationTokenRepository.GetByIdAsync(validatePhoneValidationToken.Id);

      if (_phoneValidationToken == null)
        return this.BadRequest(new Error("Phone validation token is invalid.", code: "PhoneValidationTokens003"));

      if (_phoneValidationToken.Code != validatePhoneValidationToken.Code)
        return this.BadRequest(new Error("Code is invalid.", code: "PhoneValidationTokens004"));

      if (_phoneValidationToken.Validated != null)
        return this.BadRequest(new Error("Phone validation token is already validated.", code: "PhoneValidationTokens005"));

      if (_phoneValidationToken.Validated < DateTime.Now.AddMinutes(3) || _phoneValidationToken.Used != null)
        return this.BadRequest(new Error("Phone validation token expired.", code: "PhoneValidationTokens006"));

      _phoneValidationToken.Validated = DateTime.Now;
      this.PhoneValidationTokenRepository.Edit(_phoneValidationToken);
      await this.Storage.SaveAsync();
      return this.NoContent();
    }
  }
}