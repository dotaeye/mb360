using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Description;

using System.Data.Entity.Spatial;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MB.Data.Service;
using MB.Data.DTO;
using MB.Data.AutoMapper;
using MB.Data.Models;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Cryptography;
using SQ.Core.Data;
using SQ.Core.UI;
using SQ.Core.Caching;
using MB.Models;
using MB.Helpers;
using System.Text;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace MB.Controllers
{
    [RoutePrefix("api/app")]
    public class AppController : ApiController
    {
        private ICategoryService CategoryService;
        private IBannerService BannerService;
        private IManufacturerService ManufacturerService;
        private IProductService ProductService;
        private ISpecificationAttributeOptionService SpecificationAttributeOptionService;
        private ICacheManager CacheManager;
        public AppController(
             ICategoryService _CategoryService,
             IBannerService _BannerService,
             IManufacturerService _ManufacturerService,
             IProductService _ProductService,
             ISpecificationAttributeOptionService _SpecificationAttributeOptionService,
             ICacheManager _CacheManager
          )
        {
            this.CategoryService = _CategoryService;
            this.BannerService = _BannerService;
            this.ManufacturerService = _ManufacturerService;
            this.ProductService = _ProductService;
            this.SpecificationAttributeOptionService = _SpecificationAttributeOptionService;
            this.CacheManager = _CacheManager;
        }


        [Route("home")]
        [HttpGet]
        [ResponseType(typeof(HomeModel))]
        public ApiResult<HomeModel> home(int number = 6)
        {
            var result = new ApiResult<HomeModel>();
            var model = new HomeModel();
            model.HotCategories = CategoryService.GetAll()
                .Where(x => !x.Deleted && x.IsHot)
                .ProjectTo<CategoryDTO>()
                .OrderBy(x => x.HotOrder)
                .Take(number).ToList();

            model.HotManufacturers = ManufacturerService.GetAll()
                .Where(x => !x.Deleted && x.IsHot)
                .ProjectTo<ManufacturerDTO>()
                .OrderBy(x => x.HotOrder)
                .Take(number).ToList();

            model.Banners = BannerService.GetAll()
              .Where(x => !x.Deleted)
              .ProjectTo<BannerDTO>()
              .OrderByDescending(x => x.Id)
              .Take(number).ToList();
            result.Data = model;
            //
            return result;
        }

        //[Route("key")]
        //[HttpGet]
        //public IHttpActionResult GetKey()
        //{

        //    string decryptionKey = CreateKey(24);
        //    string validationKey = CreateKey(20);

        //    return Ok(new
        //    {
        //        validationKey = validationKey,
        //        decryptionKey = decryptionKey
        //    });
        //}


        //[Route("token")]
        //[HttpGet]
        //public IHttpActionResult GetTestToken()
        //{
        //    ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

        //    identity.AddClaim(new Claim(ClaimTypes.Name, "123"));

        //    var props = new AuthenticationProperties()
        //    {
        //        IssuedUtc = DateTime.UtcNow,
        //        ExpiresUtc = DateTime.UtcNow.AddDays(14),
        //    };

        //    var ticket = new AuthenticationTicket(identity, props);

        //    var accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);

        //    return Ok(accessToken);
        //}




        //private string CreateKey(int numBytes)
        //{
        //    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //    byte[] buff = new byte[numBytes];

        //    rng.GetBytes(buff);
        //    return BytesToHexString(buff);
        //}

        //private string BytesToHexString(byte[] bytes)
        //{
        //    StringBuilder hexString = new StringBuilder(64);

        //    for (int counter = 0; counter < bytes.Length; counter++)
        //    {
        //        hexString.Append(String.Format("{0:X2}", bytes[counter]));
        //    }
        //    return hexString.ToString();
        //}
    }
}
