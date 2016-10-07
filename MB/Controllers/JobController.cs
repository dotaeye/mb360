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
    [RoutePrefix("api/Job")]
    public class JobController : ApiController
    {
        private IJobService JobService;
        private IDepartmentService DepartmentService;
        public JobController(
            IJobService _JobService,
            IDepartmentService _DepartmentService
          )
        {
            this.JobService = _JobService;
            this.DepartmentService = _DepartmentService;
        }

        [Route("")]
        public ApiListResult<JobDTO> Get([FromUri] AntPageOption option = null)
        {
            var query = JobService.GetAll().Where(x => !x.Deleted).ProjectTo<JobDTO>();
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
            var result = query.Paging<JobDTO>(option.Page - 1, option.Results, count);
            return new ApiListResult<JobDTO>(result, result.PageIndex, result.PageSize, count);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(JobDTO))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            JobDTO Job = await JobService.GetAll().Where(x => x.Id == id && !x.Deleted).ProjectTo<JobDTO>().FirstOrDefaultAsync();
            if (Job == null)
            {
                return NotFound();
            }
            return Ok(Job);
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(JobDTO))]
        public async Task<IHttpActionResult> Create([FromBody]JobDTO JobDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = JobDto.ToEntity();

            entity.CreateUserId = User.Identity.GetUserId();
            entity.CreateTime = DateTime.Now;
            await JobService.InsertAsync(entity);
            return Ok(entity.ToModel());
        }


        [Route("")]
        [HttpPut]
        [ResponseType(typeof(JobDTO))]
        public async Task<IHttpActionResult> Update([FromBody]JobDTO JobDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await JobService.FindOneAsync(JobDto.Id);
            entity = JobDto.ToEntity(entity);
            entity.LastUserId = User.Identity.GetUserId();
            entity.LastTime = DateTime.Now;
            await JobService.UpdateAsync(entity);
            return Ok(entity.ToModel());
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(JobDTO))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Job entity = await JobService.FindOneAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await JobService.DeleteAsync(entity);

            return Ok(entity.ToModel());
        }


        [Route("cascader/{id:int=0}")]
        public List<Cascader> GetJobCascader(int Id)
        {
            var cascader = new List<Cascader>();
            GenerateCascader(null, Id, cascader);
            return cascader;
        }


        private void GenerateCascader(int? Id, int currentId, List<Cascader> cascader)
        {
            var query = DepartmentService.GetAll().Where(x => x.Id != currentId && !x.Deleted);
            if (Id.HasValue)
            {
                query = query.Where(x => x.ParentId == Id.Value);
            }
            else
            {
                query = query.Where(x => x.ParentId.Equals(null));
            }

            var departments = query.ToList();

            foreach (var depart in departments)
            {
                var item = new Cascader()
                {
                    Label = depart.Name,
                    Key = "d_" + depart.Id.ToString(),
                    Value = "d_" + depart.Id.ToString(),
                    ParentId = depart.ParentId.HasValue ? depart.ParentId.Value.ToString() : null
                };

                if (DepartmentService.GetAll().Any(x => x.ParentId == depart.Id && x.Id != currentId && !x.Deleted))
                {
                    if (item.Children == null)
                    {
                        item.Children = new List<Cascader>();
                    }
                    GenerateCascader(depart.Id, currentId, item.Children);
                }

                if (JobService.GetAll().Any(x => x.DepartmentId == depart.Id && !x.Deleted))
                {

                    cascader.Add(item);

                    if (item.Children == null)
                    {
                        item.Children = new List<Cascader>();
                    }

                    var jobs = JobService.GetAll().Where(x => x.DepartmentId == depart.Id && !x.Deleted).ToList();

                    foreach (var job in jobs)
                    {
                        var jobItem = new Cascader()
                        {
                            Label = job.Name,
                            Value = job.Id.ToString(),
                            Key = job.Id.ToString(),
                            ParentId = item.Value
                        };
                        item.Children.Add(jobItem);
                    }
                }

               
            }
        }
    }
}




