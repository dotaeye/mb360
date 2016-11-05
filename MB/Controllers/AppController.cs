﻿using System;
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
    public class AppController : ApiController
    {
        private ICategoryService CategoryService;
        private IManufacturerService ManufacturerService;
        private IProductService ProductService;
        private ISpecificationAttributeOptionService SpecificationAttributeOptionService;
        private ICacheManager CacheManager;
        public AppController(
             ICategoryService _CategoryService,
             IManufacturerService _ManufacturerService,
             IProductService _ProductService,
             ISpecificationAttributeOptionService _SpecificationAttributeOptionService,
             ICacheManager _CacheManager
          )
        {
            this.CategoryService = _CategoryService;
            this.ManufacturerService = _ManufacturerService;
            this.ProductService = _ProductService;
            this.SpecificationAttributeOptionService = _SpecificationAttributeOptionService;
            this.CacheManager = _CacheManager;
        }


        [Route("home")]
        [HttpGet]
        [ResponseType(typeof(HomeModel))]
        public HomeModel home(int number = 6)
        {
            var model = new HomeModel();
            model.HotCategoires= CategoryService.GetAll()
                .Where(x => !x.Deleted && x.IsHot)
                .ProjectTo<CategoryDTO>()
                .OrderBy(x => x.HotOrder)
                .Take(number).ToList();

            model.HotManufacturers=ManufacturerService.GetAll()
                .Where(x => !x.Deleted && x.IsHot)
                .ProjectTo<ManufacturerDTO>()
                .OrderBy(x => x.HotOrder)
                .Take(number).ToList();
            return model;
        }
    }
}
