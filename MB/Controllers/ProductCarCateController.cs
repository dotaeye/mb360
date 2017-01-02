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
using MB.Models;

namespace MB.Controllers
{
    [RoutePrefix("api/ProductCarCate")]
    public class ProductCarCateController : ApiController
    {
        private IProductCarCateService ProductCarCateService;
        private ICarCateService CarCateService;
        public ProductCarCateController(
            IProductCarCateService _ProductCarCateService,
            ICarCateService _CarCateService
          )
        {
            this.ProductCarCateService = _ProductCarCateService;
            this.CarCateService = _CarCateService;
        }

        [Route("")]
        public List<ProductCarCateItem> Get([FromUri] ProductCarCatePageOption option = null)
        {
            var result = new List<ProductCarCateItem>();
            var parentCategories = CarCateService.GetAll().Where(x => !x.Deleted && x.ParentId == option.CarCateId).ToList();
            foreach (var category in parentCategories)
            {
                var categories = CarCateService.GetAll().Where(x => !x.Deleted && x.ParentId == category.Id).ToList();
                var ids = categories.Select(x => x.Id).ToList();
                var productCarCateIds = ProductCarCateService.GetAll().Where(x => x.ProductId == option.ProductId && ids.Contains(x.CarCateId)).Select(x => x.CarCateId).ToList();
                result.AddRange(categories.Select(x => new ProductCarCateItem()
                {
                    CarCateId = x.Id,
                    ProductId = option.ProductId,
                    Id = productCarCateIds.Contains(x.Id) ? x.Id : 0,
                    Selected = productCarCateIds.Contains(x.Id),
                    CarName = category.Name + " | " + x.Name

                }).ToList());
            }
            return result;
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody]ProductCarCateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entities = model.Ids.Select(x => new ProductCarCate()
            {
                ProductId = model.ProductId,
                CarCateId = x
            }).ToList();

            await ProductCarCateService.InsertAsync(entities);

            return Ok();
        }

        [Route("")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromBody]ProductCarCateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entities = ProductCarCateService.GetAll().Where(x => model.Ids.Contains(x.Id)).ToList();

            await ProductCarCateService.DeleteAsync(entities);

            return Ok();
        }

    }
}




