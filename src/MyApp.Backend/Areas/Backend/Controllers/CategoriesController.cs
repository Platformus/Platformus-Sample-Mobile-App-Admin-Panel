// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Backend.ViewModels.Categories;
using MyApp.Data.Entities;
using MyApp.Filters;
using Platformus;

namespace MyApp.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageCategoriesPermission)]
  public class CategoriesController : Platformus.Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Category, CategoryFilter> Repository
    {
      get => this.Storage.GetRepository<int, Category, CategoryFilter>();
    }

    public CategoriesController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]CategoryFilter filter = null, string sorting = "+position", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(
        sorting, offset, limit, await this.Repository.CountAsync(filter),
        await this.Repository.GetAllAsync(
          filter, sorting, offset, limit,
          new Inclusion<Category>(c => c.Name.Localizations)
        )
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(CreateOrEditViewModelFactory.Create(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
          (int)id,
          new Inclusion<Category>(c => c.Name.Localizations)
        )
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Category category = CreateOrEditViewModelMapper.Map(
          createOrEdit.Id == null ? new Category() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        await this.CreateOrEditEntityLocalizationsAsync(category);

        if (createOrEdit.Id == null)
          this.Repository.Create(category);

        else this.Repository.Edit(category);

        await this.Storage.SaveAsync();
        return this.Redirect(this.Request.CombineUrl("/backend/categories"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<ActionResult> DeleteAsync(int id)
    {
      Category category = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(category.Id);
      await this.Storage.SaveAsync();
      return this.RedirectToAction("Index");
    }
  }
}