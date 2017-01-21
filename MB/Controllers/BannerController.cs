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
using MB.Filters;

namespace MB.Controllers
{
    [MbAuthorize]
    [RoutePrefix("api/Banner")]
    public class BannerController : ApiController
    {
        private IBannerService BannerService;
        public BannerController(
            IBannerService _BannerService
          )
        {
            this.BannerService = _BannerService;
        }

        [Route("")]
        public ApiListResult<BannerDTO> Get([FromUri] AntPageOption option = null)
        {
            var query = BannerService.GetAll().Where(x => !x.Deleted).ProjectTo<BannerDTO>();
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
            var result = query.Paging<BannerDTO>(option.Page - 1, option.Results, count);
            return new ApiListResult<BannerDTO>(result, result.PageIndex, result.PageSize, count);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(BannerDTO))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            BannerDTO Banner = await BannerService.GetAll().Where(x => x.Id == id && !x.Deleted).ProjectTo<BannerDTO>().FirstOrDefaultAsync();
            if (Banner == null)
            {
                return NotFound();
            }
            return Ok(Banner);
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(BannerDTO))]
        public async Task<IHttpActionResult> Create([FromBody]BannerDTO BannerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = BannerDto.ToEntity();

            entity.CreateUserId = User.Identity.GetUserId();
            entity.CreateTime = DateTime.Now;
            await BannerService.InsertAsync(entity);
            return Ok(entity.ToModel());
        }


        [Route("")]
        [HttpPut]
        [ResponseType(typeof(BannerDTO))]
        public async Task<IHttpActionResult> Update([FromBody]BannerDTO BannerDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await BannerService.FindOneAsync(BannerDto.Id);
            entity = BannerDto.ToEntity(entity);
            entity.LastUserId = User.Identity.GetUserId();
            entity.LastTime = DateTime.Now;
            await BannerService.UpdateAsync(entity);
            return Ok(entity.ToModel());
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(BannerDTO))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Banner entity = await BannerService.FindOneAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await BannerService.DeleteAsync(entity);

            return Ok(entity.ToModel());
        }

    }
}




