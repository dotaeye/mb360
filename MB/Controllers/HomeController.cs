using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Data;
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
    public class HomeController : Controller
    {
        private ICategoryService CategoryService;
        private IProductService ProductService;
        private ISpecificationAttributeOptionService SpecificationAttributeOptionService;
        private ICacheManager CacheManager;
        public HomeController(
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

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Topic()
        {
            return View();
        }



    }
}
