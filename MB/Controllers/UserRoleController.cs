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
using MB.Helpers;

using MB.Filters;

namespace MB.Controllers
{
    [MbAuthorize]
    [RoutePrefix("api/UserRole")]
    public class UserRoleController : ApiController
    {
        private IUserRoleService UserRoleService;
        private IUserPermissionService UserPermissionService;
        public UserRoleController(
            IUserRoleService _UserRoleService,
            IUserPermissionService _UserPermissionService
          )
        {
            this.UserRoleService = _UserRoleService;
            this.UserPermissionService = _UserPermissionService;
        }

        [Route("")]
        public ApiListResult<UserRoleDTO> Get([FromUri] AntPageOption option = null)
        {
             var query = UserRoleService.GetAll().Where(x => !x.Deleted).ProjectTo<UserRoleDTO>();
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
            var result = query.Paging<UserRoleDTO>(option.Page - 1, option.Results, count);
            return new ApiListResult<UserRoleDTO>(result, result.PageIndex, result.PageSize, count);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(UserRoleDTO))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var entity= await UserRoleService.GetAll().Include(x=>x.UserPermissions).Where(x => x.Id == id&&!x.Deleted).FirstOrDefaultAsync();
            UserRoleDTO UserRole = entity.ToModel();
            UserRole.Permission = entity.UserPermissions.Select(x => x.Id).ToList();
            if (UserRole == null)
            {
                return NotFound();
            }
            return Ok(UserRole);
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(UserRoleDTO))]
        public async Task<IHttpActionResult> Create([FromBody]UserRoleDTO UserRoleDto)
        {
		    if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = UserRoleDto.ToEntity();
            entity.CreateUserId = User.Identity.GetUserId();
            entity.CreateTime = DateTime.Now;
            var permission = UserPermissionService.GetAll().Where(x => UserRoleDto.Permission.Contains(x.Id)).ToList();
            entity.UserPermissions = permission;
            await UserRoleService.InsertAsync(entity);
            return Ok(entity.ToModel());
        }


        [Route("")]
        [HttpPut]
        [ResponseType(typeof(UserRoleDTO))]
        public async Task<IHttpActionResult> Update([FromBody]UserRoleDTO UserRoleDto)
        {
			if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = UserRoleService.GetAll().Include(x => x.UserPermissions).SingleOrDefault(x => x.Id == UserRoleDto.Id);
            entity = UserRoleDto.ToEntity(entity);
            entity.LastUserId = User.Identity.GetUserId();
            entity.LastTime = DateTime.Now;

            var permission = UserPermissionService.GetAll().Where(x => UserRoleDto.Permission.Contains(x.Id)).ToList();
            var currentIds = entity.UserPermissions.Select(x => x.Id).ToList();
            foreach (UserPermission ps in UserPermissionService.GetAll())
            {
                if (permission.Count(x => x.Id == ps.Id) > 0)
                {
                    if (!currentIds.Contains(ps.Id))
                    {
                        entity.UserPermissions.Add(ps);
                    }
                }
                else
                {
                    if (currentIds.Contains(ps.Id))
                    {
                        entity.UserPermissions.Remove(ps);
                    }
                }
            }
            await UserRoleService.UpdateAsync(entity);
            return Ok(entity.ToModel());
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(UserRoleDTO))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            UserRole entity = await UserRoleService.FindOneAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await UserRoleService.DeleteAsync(entity);

            return Ok(entity.ToModel());
        }

        [Route("selectlist")]
        [HttpGet]
        public List<UserRoleDTO> GetStorageSelectList()
        {
            var result = new List<UserRoleDTO>();
            result = UserRoleService.GetAll().Where(x => !x.Deleted).ProjectTo<UserRoleDTO>().ToList();
            return result;
        }
    }
}

