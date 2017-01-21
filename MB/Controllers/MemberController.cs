using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Linq;
using System.Data.Entity;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using MB.Models;
using MB.Providers;
using MB.Results;
using MB.Data.Service;
using MB.Data.DTO;
using MB.Data.Models;
using SQ.Core.Data;
using AutoMapper.QueryableExtensions;
using System.Web.Http.Description;
using MB.Data.AutoMapper;

using MB.Filters;

namespace MB.Controllers
{
    [MbAuthorize]
    [RoutePrefix("api/Member")]
    public class MemberController : ApiController
    {
        private ApplicationUserManager _userManager;

        private IUserRoleService UserRoleService;
        private IUserPermissionService UserPermissionService;
        public MemberController(
             IUserRoleService _UserRoleService,
            IUserPermissionService _UserPermissionService)
        {
            this.UserRoleService = _UserRoleService;
            this.UserPermissionService = _UserPermissionService;
        }

        public MemberController(
            ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [Route("")]
        public ApiListResult<MemberDTO> Get([FromUri] AntPageOption option = null)
        {
            var query = UserManager.Users.Where(x => !x.Deleted).ProjectTo<MemberDTO>();
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
            var result = query.Paging<MemberDTO>(option.Page - 1, option.Results, count);
            return new ApiListResult<MemberDTO>(result, result.PageIndex, result.PageSize, count);
        }

        [Route("{id:guid}")]
        [ResponseType(typeof(MemberDTO))]
        public async Task<IHttpActionResult> GetById(string id)
        {
            MemberDTO Member = await UserManager.Users.Where(x => x.Id == id && !x.Deleted).ProjectTo<MemberDTO>().FirstOrDefaultAsync();

            if (Member == null)
            {
                return NotFound();
            }

            return Ok(Member);
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(MemberDTO))]
        public async Task<IHttpActionResult> Create([FromBody]MemberDTO MemberDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var entity = MemberDto.ToEntity();
                entity.Id = Guid.NewGuid().ToString();
                entity.UserName = entity.PhoneNumber;
                entity.Email = entity.PhoneNumber;
                entity.CreateTime = DateTime.Now;
                entity.CreateUserId = User.Identity.GetUserId();
                IdentityResult result = await UserManager.CreateAsync(entity, MemberDto.Password);
                return Ok(entity.ToModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("")]
        [HttpPut]
        [ResponseType(typeof(MemberDTO))]
        public async Task<IHttpActionResult> Update([FromBody]MemberDTO MemberDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await UserManager.FindByIdAsync(MemberDto.Id);
      
            entity.Email = MemberDto.UserName;
            entity.UserName = MemberDto.UserName;
            entity.PhoneNumber = MemberDto.PhoneNumber;
            entity.PhoneNumberConfirmed = MemberDto.PhoneNumberConfirmed;
            entity.EmailConfirmed = MemberDto.EmailConfirmed;
            entity.JobId = MemberDto.JobId;
            entity.UserRoleId = MemberDto.UserRoleId;
            entity.LastTime = DateTime.Now;
            entity.LastUserId = User.Identity.GetUserId();
            var result = await UserManager.UpdateAsync(entity);
            return Ok(entity.ToModel());
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(MemberDTO))]
        public async Task<IHttpActionResult> Delete(string id)
        {
            var entity = await UserManager.FindByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Deleted = true;
            await UserManager.UpdateAsync(entity);
            return Ok(entity.ToModel());
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // 没有可发送的 ModelState 错误，因此仅返回空 BadRequest。
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}




