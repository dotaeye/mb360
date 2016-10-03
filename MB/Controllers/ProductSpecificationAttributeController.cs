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
    [RoutePrefix("api/ProductSpecificationAttribute")]
    public class ProductSpecificationAttributeController : ApiController
    {
        private IProductSpecificationAttributeService ProductSpecificationAttributeService;
        public ProductSpecificationAttributeController(
            IProductSpecificationAttributeService _ProductSpecificationAttributeService
          )
        {
            this.ProductSpecificationAttributeService = _ProductSpecificationAttributeService;
        }

        [Route("")]
        public ApiListResult<ProductSpecificationAttributeDTO> Get([FromUri] AntPageOption option = null)
        {
            var query = ProductSpecificationAttributeService.GetAll().Where(x=>x.ProductId==option.Id).ProjectTo<ProductSpecificationAttributeDTO>();
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
            var result = query.Paging<ProductSpecificationAttributeDTO>(option.Page - 1, option.Results, count);
            return new ApiListResult<ProductSpecificationAttributeDTO>(result, result.PageIndex, result.PageSize, count);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(ProductSpecificationAttributeDTO))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            ProductSpecificationAttributeDTO ProductSpecificationAttribute = await ProductSpecificationAttributeService.GetAll().Where(x => x.Id == id ).ProjectTo<ProductSpecificationAttributeDTO>().FirstOrDefaultAsync();
            if (ProductSpecificationAttribute == null)
            {
                return NotFound();
            }
            return Ok(ProductSpecificationAttribute);
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(ProductSpecificationAttributeDTO))]
        public async Task<IHttpActionResult> Create([FromBody]ProductSpecificationAttributeDTO ProductSpecificationAttributeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = ProductSpecificationAttributeDto.ToEntity();

            await ProductSpecificationAttributeService.InsertAsync(entity);
            return Ok(entity.ToModel());
        }


        [Route("")]
        [HttpPut]
        [ResponseType(typeof(ProductSpecificationAttributeDTO))]
        public async Task<IHttpActionResult> Update([FromBody]ProductSpecificationAttributeDTO ProductSpecificationAttributeDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await ProductSpecificationAttributeService.FindOneAsync(ProductSpecificationAttributeDto.Id);
            entity = ProductSpecificationAttributeDto.ToEntity(entity);
          
            await ProductSpecificationAttributeService.UpdateAsync(entity);
            return Ok(entity.ToModel());
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(ProductSpecificationAttributeDTO))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            ProductSpecificationAttribute entity = await ProductSpecificationAttributeService.FindOneAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await ProductSpecificationAttributeService.DeleteAsync(entity);

            return Ok(entity.ToModel());
        }

    }
}




