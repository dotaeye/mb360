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
    [RoutePrefix("api/Address")]
    public class AddressController : ApiController
    {
        private IAddressService AddressService;
        public AddressController(
            IAddressService _AddressService
          )
        {
            this.AddressService = _AddressService;
        }

        [Route("")]
        public ApiListResult<AddressDTO> Get([FromUri] AntPageOption option = null)
        {
            var query = AddressService.GetAll().Where(x => !x.Deleted).ProjectTo<AddressDTO>();
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
            var result = query.Paging<AddressDTO>(option.Page - 1, option.Results, count);
            return new ApiListResult<AddressDTO>(result, result.PageIndex, result.PageSize, count);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(AddressDTO))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            AddressDTO Address = await AddressService.GetAll().Where(x => x.Id == id && !x.Deleted).ProjectTo<AddressDTO>().FirstOrDefaultAsync();
            if (Address == null)
            {
                return NotFound();
            }
            return Ok(Address);
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(AddressDTO))]
        public async Task<IHttpActionResult> Create([FromBody]AddressDTO AddressDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = AddressDto.ToEntity();

            entity.CreateUserId = User.Identity.GetUserId();
            entity.CreateTime = DateTime.Now;
            await AddressService.InsertAsync(entity);
            return Ok(entity.ToModel());
        }


        [Route("")]
        [HttpPut]
        [ResponseType(typeof(AddressDTO))]
        public async Task<IHttpActionResult> Update([FromBody]AddressDTO AddressDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await AddressService.FindOneAsync(AddressDto.Id);
            entity = AddressDto.ToEntity(entity);
            entity.LastUserId = User.Identity.GetUserId();
            entity.LastTime = DateTime.Now;
            await AddressService.UpdateAsync(entity);
            return Ok(entity.ToModel());
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(AddressDTO))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Address entity = await AddressService.FindOneAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await AddressService.DeleteAsync(entity);

            return Ok(entity.ToModel());
        }

    }
}




