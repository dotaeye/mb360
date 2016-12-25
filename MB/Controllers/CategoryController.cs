﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity.Spatial;
using Microsoft.AspNet.Identity;
using MB.Data.Service;
using MB.Data.DTO;
using MB.Data.AutoMapper;
using MB.Data.Models;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using SQ.Core.Data;
using SQ.Core.UI;
using SQ.Core.Caching;
using MB.Models;
using MB.Helpers;

namespace MB.Controllers
{
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private ICategoryService CategoryService;
        private IProductService ProductService;
        private ISpecificationAttributeOptionService SpecificationAttributeOptionService;
        private ICacheManager CacheManager;
        public CategoryController(
            ICategoryService _CategoryService,
             IProductService _ProductService,
             ISpecificationAttributeOptionService _SpecificationAttributeOptionService,
             ICacheManager _CacheManager
          )
        {
            this.CategoryService = _CategoryService;
            this.ProductService = _ProductService;
            this.SpecificationAttributeOptionService = _SpecificationAttributeOptionService;
            this.CacheManager = _CacheManager;
        }

        [Route("")]
        public ApiListResult<CategoryDTO> Get([FromUri] AntPageOption option = null)
        {
            var query = CategoryService.GetAll().Where(x => !x.Deleted).ProjectTo<CategoryDTO>();
            if (option != null)
            {
                if (!string.IsNullOrEmpty(option.SortField))
                {
                    //for example
                    if (option.SortField == "id")
                    {
                        if (option.SortOrder == PageSortTyoe.DESC)
                        {
                            query = query.OrderByDescending(x => x.Id);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.Id);
                        }
                    }
                    else if (option.SortField == "code")
                    {

                        if (option.SortOrder == PageSortTyoe.DESC)
                        {
                            query = query.OrderByDescending(x => x.Code);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.Code);
                        }
                    }
                    else if (option.SortField == "hotOrder")
                    {

                        if (option.SortOrder == PageSortTyoe.DESC)
                        {
                            query = query.OrderByDescending(x => x.HotOrder);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.HotOrder);
                        }
                    }
                }

                if (option.Page > 0 && option.Results > 0)
                {
                    if (string.IsNullOrEmpty(option.SortField))
                    {
                        query = query.OrderBy(x => x.Id);
                    }
                }
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }
            var count = query.Count();
            var result = query.Paging<CategoryDTO>(option.Page - 1, option.Results, count);
            return new ApiListResult<CategoryDTO>(result, result.PageIndex, result.PageSize, count);
        }

        [Route("cascader/{id:int=0}")]
        public List<Cascader> GetCategoryCascader(int Id)
        {
            var cascader = new List<Cascader>();
            GenerateCascader(null, Id, cascader);
            return cascader;
        }


        private void GenerateCascader(int? Id, int currentId, List<Cascader> cascader)
        {
            var query = CategoryService.GetAll().Where(x => x.Id != currentId && !x.Deleted);
            if (Id.HasValue)
            {
                query = query.Where(x => x.ParentId == Id.Value);
            }
            else
            {
                query = query.Where(x => x.ParentId.Equals(null));
            }



            var dategorys = query.ToList();

            foreach (var depart in dategorys)
            {
                var item = new Cascader()
                {
                    Label = depart.Name,
                    Value = depart.Id.ToString(),
                    ParentId = depart.ParentId.HasValue ? depart.ParentId.Value.ToString() : null
                };

                cascader.Add(item);

                if (CategoryService.GetAll().Any(x => x.ParentId == depart.Id && x.Id != currentId && !x.Deleted))
                {
                    item.Children = new List<Cascader>();
                    GenerateCascader(depart.Id, currentId, item.Children);
                }
            }
        }


        [Route("{id:int}")]
        [ResponseType(typeof(CategoryDTO))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            CategoryDTO Category = await CategoryService.GetAll().Where(x => x.Id == id && !x.Deleted).ProjectTo<CategoryDTO>().FirstOrDefaultAsync();
            if (Category == null)
            {
                return NotFound();
            }
            return Ok(Category);
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(CategoryDTO))]
        public async Task<IHttpActionResult> Create([FromBody]CategoryDTO CategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = CategoryDto.ToEntity();

            entity.CreateUserId = User.Identity.GetUserId();
            entity.CreateTime = DateTime.Now;
            await CategoryService.InsertAsync(entity);
            return Ok(entity.ToModel());
        }


        [Route("")]
        [HttpPut]
        [ResponseType(typeof(CategoryDTO))]
        public async Task<IHttpActionResult> Update([FromBody]CategoryDTO CategoryDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await CategoryService.FindOneAsync(CategoryDto.Id);
            entity = CategoryDto.ToEntity(entity);
            entity.LastUserId = User.Identity.GetUserId();
            entity.LastTime = DateTime.Now;
            await CategoryService.UpdateAsync(entity);
            return Ok(entity.ToModel());
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(CategoryDTO))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Category entity = await CategoryService.FindOneAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await CategoryService.DeleteAsync(entity);

            return Ok(entity.ToModel());
        }



        [Route("list/")]
        [HttpGet]
        public async Task<IHttpActionResult> List(
            int categoryId = 0,
            int manufacturerId = 0,
            int carId = 0,
            string keywords = null,
            string specs = null,
            int pageIndex = 0,
            int pageSize = 0,
            string location = null,
            int? orderBy = null,
            decimal? minPrice = null,
            decimal? maxPrice = null
            )
        {
            var result = new ApiResult<CategoryModel>();
            var model = new CategoryModel();
            var categoryIds = new List<int>();
            DbGeography Location = null;

            if (!string.IsNullOrEmpty(location))
            {
                Location = DbGeography.FromText(string.Format("POINT({0})", location));
            }
            if (categoryId != 0)
            {
                Category entity = await CategoryService.FindOneAsync(categoryId);
                model = new CategoryModel()
                {
                    Id = entity.Id,
                    Description = entity.Description,
                    Name = entity.Name,
                    ImageUrl = entity.ImageUrl
                };
                model.SubCategories = CategoryService.GetAll()
                   .Where(x => !x.Deleted && x.ParentId.Value == entity.Id)
                   .ProjectTo<CategoryDTO>().OrderBy(x => x.DisplayOrder).ToList();

                categoryIds.Add(entity.Id);
                categoryIds.AddRange(model.SubCategories.Select(x => x.Id));
            }

            if (pageSize == 0)
            {
                pageSize = 20;
            }

            int RoleId = 2;

            IList<int> alreadyFilteredSpecOptionIds = MBHelper.StringToIds(specs);
            IList<int> filterableSpecificationAttributeOptionIds;
            var loadFilterableSpecificationAttributeOptionIds = pageIndex == 0;
            var products = ProductService.SearchProducts(
                out filterableSpecificationAttributeOptionIds,
                categoryIds: categoryIds,
                manufacturerId: manufacturerId,
                carId: carId,
                loadFilterableSpecificationAttributeOptionIds: loadFilterableSpecificationAttributeOptionIds,
                showHidden: true,
                location: Location,
                featuredProducts: null,
                //RoleId: MBHelper.GetUserRoleId(User),
                RoleId: RoleId,
                priceMin: minPrice,
                priceMax: maxPrice,
                filteredSpecs: alreadyFilteredSpecOptionIds,
                //orderBy: (ProductSortingEnum)command.OrderBy,
                orderBy: orderBy.HasValue ? (ProductSortingEnum)orderBy : ProductSortingEnum.Position,
                pageIndex: pageIndex,
                pageSize: pageSize);
            //model.Products = PrepareProductOverviewModels(products).ToList();
            model.Products = products.Select(x => new ProductOverviewModel()
            {
                Description = x.Description,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Price = x.Price,
                VipPrice = x.VipPrice,
                Distance = x.Distance

            }).ToList();
            model.TotalCount = products.TotalCount;
            if (loadFilterableSpecificationAttributeOptionIds)
            {
                model.PrepareSpecsFilters(alreadyFilteredSpecOptionIds,
                   filterableSpecificationAttributeOptionIds != null ? filterableSpecificationAttributeOptionIds.ToArray() : null,
                   SpecificationAttributeOptionService, CacheManager);
            }
            result.Data = model;
            return Ok(result);
        }

        [Route("all")]
        [HttpGet]
        [ResponseType(typeof(ApiResult<List<Cascader>>))]
        public ApiResult<List<Cascader>> All()
        {
            var result = new ApiResult<List<Cascader>>();

            var cascader = new List<Cascader>();

            try
            {

                var query = CategoryService.GetAll().Where(x => !x.Deleted && x.Level == 2).ToList();

                var categories = query.ToList();

                foreach (var cate in categories)
                {
                    var item = new Cascader()
                    {
                        Label = cate.Name,
                        Value = cate.Id.ToString(),
                        ParentId = cate.ParentId.HasValue ? cate.ParentId.Value.ToString() : null,
                        ImageUrl = cate.ImageUrl
                    };

                    item.Children = new List<Cascader>();

                    var childCategories = CategoryService.GetAll().Where(x => !x.Deleted && x.Level == 3 && x.ParentId == cate.Id).ToList();

                    if (childCategories.Count > 0)
                    {
                        item.Children = childCategories.Select(x => new Cascader()
                        {
                            Label = x.Name,
                            Value = x.Id.ToString(),
                            ParentId = x.ParentId.HasValue ? x.ParentId.Value.ToString() : null,
                            ImageUrl = x.ImageUrl

                        }).ToList();

                        cascader.Add(item);
                    }
                }
                result.Data = cascader;
            }
            catch (Exception ex)
            {
                result.Info = ex.Message;
                result.Code = 1;
                return result;
            }

            result.Info = "获取类别成功！";
            return result;

        }
    }
}




