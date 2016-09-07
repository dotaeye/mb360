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
using Microsoft.AspNet.Identity;
using MB.Data.Service;
using MB.Data.DTO;
using MB.Data.AutoMapper;
using MB.Data.Models;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using SQ.Core.Data;
using SQ.Core.UI;

namespace MB.Controllers
{
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private ICategoryService CategoryService;
        public CategoryController(
            ICategoryService _CategoryService
          )
        {
            this.CategoryService = _CategoryService;
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
            var query = CategoryService.GetAll().Where(x => x.Id != currentId);
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

                if (CategoryService.GetAll().Any(x => x.ParentId == depart.Id && x.Id != currentId))
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

    }
}




