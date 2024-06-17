// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus;
using WebApplication.Backend.ViewModels.Products;
using WebApplication.Data.Entities;
using WebApplication.Filters;

namespace WebApplication.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageProductsPermission)]
  public class ProductsController : Platformus.Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Product, ProductFilter> Repository
    {
      get => this.Storage.GetRepository<int, Product, ProductFilter>();
    }

    public ProductsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]ProductFilter filter = null, string sorting = null, int offset = 0, int limit = 10)
    {
      if (string.IsNullOrEmpty(sorting))
        sorting = "+" + this.HttpContext.CreateLocalizedSorting("Name");

      return this.View(await IndexViewModelFactory.CreateAsync(
        this.HttpContext, sorting, offset, limit, await this.Repository.CountAsync(filter),
        await this.Repository.GetAllAsync(
          filter, sorting, offset, limit,
          new Inclusion<Product>(p => p.Category.Name.Localizations),
          new Inclusion<Product>(p => p.Name.Localizations)
        )
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(await CreateOrEditViewModelFactory.CreateAsync(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
          (int)id,
          new Inclusion<Product>(p => p.Name.Localizations),
          new Inclusion<Product>(p => p.Description.Localizations)
        )
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Product product = CreateOrEditViewModelMapper.Map(
          createOrEdit.Id == null ? new Product() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(product);

        else this.Repository.Edit(product);

        await this.MergeEntityLocalizationsAsync(product);
        await this.Storage.SaveAsync();
        return this.Redirect(this.Request.CombineUrl("/backend/products"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<ActionResult> DeleteAsync(int id)
    {
      Product product = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(product.Id);
      await this.Storage.SaveAsync();
      return this.RedirectToAction("Index");
    }
  }
}