// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Domain.Services.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using WebApplication.Filters;
using WebApplication.Frontend.Dto;

namespace WebApplication.Frontend.Controllers
{
  [Route("v1/phone-validation-tokens")]
  [ApiController]
  public class PhoneValidationTokensController : Platformus.Core.Frontend.Controllers.ControllerBase
  {
    private readonly IService<Guid, Domain.Models.PhoneValidationToken, PhoneValidationTokenFilter> phoneValidationTokenService;

    private IRepository<int, Credential, CredentialFilter> CredentialRepository
    {
      get => this.Storage.GetRepository<int, Credential, CredentialFilter>();
    }

    public PhoneValidationTokensController(IStorage storage, IServiceResolver serviceResolver)
      : base(storage)
    {
      this.phoneValidationTokenService = serviceResolver.GetService<Guid, Domain.Models.PhoneValidationToken, PhoneValidationTokenFilter>();
    }

    [HttpPost]
    public async Task<ActionResult<PhoneValidationToken>> PostAsync([FromBody] CreatePhoneValidationToken createPhoneVerificationToken)
    {
      if (createPhoneVerificationToken.UserExists == true && await this.CredentialRepository.CountAsync(new CredentialFilter() { CredentialType = new CredentialTypeFilter() { Code = "phone" }, Identifier = new StringFilter(equals: createPhoneVerificationToken.Phone) }) == 0)
        return this.BadRequest(new Error("User does not exist.", code: "PhoneValidationTokens001"));

      if (createPhoneVerificationToken.UserExists == false && await this.CredentialRepository.CountAsync(new CredentialFilter() { CredentialType = new CredentialTypeFilter() { Code = "phone" }, Identifier = new StringFilter(equals: createPhoneVerificationToken.Phone) }) != 0)
        return this.BadRequest(new Error("User already exists.", code: "PhoneValidationTokens002"));

      Domain.Models.PhoneValidationToken _phoneValidationToken = createPhoneVerificationToken.ToModel();

      _phoneValidationToken.Code = "123456";
      //_phoneValidationToken.Code = new Random().Next(100000, 999999).ToString();
      _phoneValidationToken.Created = DateTime.Now;
      await this.phoneValidationTokenService.CreateAsync(_phoneValidationToken);
      return new PhoneValidationToken(_phoneValidationToken);
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync([FromBody] ValidatePhoneValidationToken validatePhoneValidationToken)
    {
      Domain.Models.PhoneValidationToken _phoneValidationToken = await this.phoneValidationTokenService.GetByIdAsync(validatePhoneValidationToken.Id);

      if (_phoneValidationToken == null)
        return this.BadRequest(new Error("Phone validation token is invalid.", code: "PhoneValidationTokens003"));

      if (_phoneValidationToken.Code != validatePhoneValidationToken.Code)
        return this.BadRequest(new Error("Code is invalid.", code: "PhoneValidationTokens004"));

      if (_phoneValidationToken.Validated != null)
        return this.BadRequest(new Error("Phone validation token is already validated.", code: "PhoneValidationTokens005"));

      if (_phoneValidationToken.Validated < DateTime.Now.AddMinutes(3) || _phoneValidationToken.Used != null)
        return this.BadRequest(new Error("Phone validation token expired.", code: "PhoneValidationTokens006"));

      _phoneValidationToken.Validated = DateTime.Now;
      await this.phoneValidationTokenService.EditAsync(_phoneValidationToken);
      return this.NoContent();
    }
  }
}