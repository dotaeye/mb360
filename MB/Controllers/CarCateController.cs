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
using SQ.Core.UI;

namespace MB.Controllers
{
    [RoutePrefix("api/CarCate")]
    public class CarCateController : ApiController
    {
        private ICarCateService CarCateService;
        public CarCateController(
            ICarCateService _CarCateService
          )
        {
            this.CarCateService = _CarCateService;
        }

        [Route("")]
        public ApiListResult<CarCateDTO> Get([FromUri] AntPageOption option = null)
        {
            var query = CarCateService.GetAll().Where(x => !x.Deleted).ProjectTo<CarCateDTO>();
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
            var result = query.Paging<CarCateDTO>(option.Page - 1, option.Results, count);
            return new ApiListResult<CarCateDTO>(result, result.PageIndex, result.PageSize, count);
        }

        [Route("cascader/{id:int=0}")]
        public List<Cascader> GetCarCateCascader(int Id)
        {
            var cascader = new List<Cascader>();
            GenerateCascader(null, Id, cascader);
            return cascader;
        }


        private void GenerateCascader(int? Id, int currentId, List<Cascader> cascader)
        {
            var query = CarCateService.GetAll().Where(x => x.Id != currentId && !x.Deleted);
            if (Id.HasValue)
            {
                query = query.Where(x => x.ParentId == Id.Value);
            }
            else
            {
                query = query.Where(x => x.ParentId.Equals(null));
            }



            var dategorys = query.ToList();

            foreach (var depart in dategorys)
            {
                var item = new Cascader()
                {
                    Label = depart.Name,
                    Value = depart.Id.ToString(),
                    ParentId = depart.ParentId.HasValue ? depart.ParentId.Value.ToString() : null
                };

                cascader.Add(item);

                if (CarCateService.GetAll().Any(x => x.ParentId == depart.Id && x.Id != currentId && !x.Deleted))
                {
                    item.Children = new List<Cascader>();
                    GenerateCascader(depart.Id, currentId, item.Children);
                }
            }
        }

        [Route("{id:int}")]
        [ResponseType(typeof(CarCateDTO))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            CarCateDTO CarCate = await CarCateService.GetAll().Where(x => x.Id == id && !x.Deleted).ProjectTo<CarCateDTO>().FirstOrDefaultAsync();
            if (CarCate == null)
            {
                return NotFound();
            }
            return Ok(CarCate);
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(CarCateDTO))]
        public async Task<IHttpActionResult> Create([FromBody]CarCateDTO CarCateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = CarCateDto.ToEntity();

            entity.CreateUserId = User.Identity.GetUserId();
            entity.CreateTime = DateTime.Now;
            await CarCateService.InsertAsync(entity);
            return Ok(entity.ToModel());
        }


        [Route("")]
        [HttpPut]
        [ResponseType(typeof(CarCateDTO))]
        public async Task<IHttpActionResult> Update([FromBody]CarCateDTO CarCateDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await CarCateService.FindOneAsync(CarCateDto.Id);
            entity = CarCateDto.ToEntity(entity);
            entity.LastUserId = User.Identity.GetUserId();
            entity.LastTime = DateTime.Now;
            await CarCateService.UpdateAsync(entity);
            return Ok(entity.ToModel());
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(CarCateDTO))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            CarCate entity = await CarCateService.FindOneAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await CarCateService.DeleteAsync(entity);

            return Ok(entity.ToModel());
        }

    }
}




