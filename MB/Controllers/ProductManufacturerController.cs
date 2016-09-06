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

namespace MB.Controllers
{
    [RoutePrefix("api/ProductManufacturer")]
    public class ProductManufacturerController : ApiController
    {
        private IProductManufacturerService ProductManufacturerService;
        public ProductManufacturerController(
            IProductManufacturerService _ProductManufacturerService
          )
        {
            this.ProductManufacturerService = _ProductManufacturerService;
        }

        [Route("")]
        public ApiListResult<ProductManufacturerDTO> Get([FromUri] AntPageOption option = null)
        {
            var query = ProductManufacturerService.GetAll().Where(x => !x.Deleted).ProjectTo<ProductManufacturerDTO>();
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
            var result = query.Paging<ProductManufacturerDTO>(option.Page - 1, option.Results, count);
            return new ApiListResult<ProductManufacturerDTO>(result, result.PageIndex, result.PageSize, count);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(ProductManufacturerDTO))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            ProductManufacturerDTO ProductManufacturer = await ProductManufacturerService.GetAll().Where(x => x.Id == id && !x.Deleted).ProjectTo<ProductManufacturerDTO>().FirstOrDefaultAsync();
            if (ProductManufacturer == null)
            {
                return NotFound();
            }
            return Ok(ProductManufacturer);
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(ProductManufacturerDTO))]
        public async Task<IHttpActionResult> Create([FromBody]ProductManufacturerDTO ProductManufacturerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = ProductManufacturerDto.ToEntity();

            entity.CreateUserId = User.Identity.GetUserId();
            entity.CreateTime = DateTime.Now;
            await ProductManufacturerService.InsertAsync(entity);
            return Ok(entity.ToModel());
        }


        [Route("")]
        [HttpPut]
        [ResponseType(typeof(ProductManufacturerDTO))]
        public async Task<IHttpActionResult> Update([FromBody]ProductManufacturerDTO ProductManufacturerDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await ProductManufacturerService.FindOneAsync(ProductManufacturerDto.Id);
            entity = ProductManufacturerDto.ToEntity(entity);
            entity.LastUserId = User.Identity.GetUserId();
            entity.LastTime = DateTime.Now;
            await ProductManufacturerService.UpdateAsync(entity);
            return Ok(entity.ToModel());
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(ProductManufacturerDTO))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            ProductManufacturer entity = await ProductManufacturerService.FindOneAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await ProductManufacturerService.DeleteAsync(entity);

            return Ok(entity.ToModel());
        }

    }
}




