﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq;
using SQ.Core.Data;
using MB.Data.Service;
using MB.Data.Models;
using System.Threading.Tasks;

namespace MB.Data.Impl
{
	public class SpecificationAttributeService : ISpecificationAttributeService
    {
		   #region Fields

        private readonly IRepository<SpecificationAttribute> _SpecificationAttributeRepository;

        #endregion

        #region Ctor

        public SpecificationAttributeService(IRepository<SpecificationAttribute> SpecificationAttributeRepository
           )
        {
            this._SpecificationAttributeRepository = SpecificationAttributeRepository;

        }
        #endregion

        public async Task<int> DeleteAsync(SpecificationAttribute entity)
        {
            if (entity == null)
                throw new ArgumentNullException("SpecificationAttribute");

            entity.Deleted = true;
           return await UpdateAsync(entity);
        }

        public async Task<SpecificationAttribute> FindOneAsync(int Id)
        {
            if (Id == 0)
                return null;

            var entity = await _SpecificationAttributeRepository.GetByIdAsync(Id);
            return entity;
        }

        public IQueryable<SpecificationAttribute> GetAll()
        {
            return _SpecificationAttributeRepository.Table;
        }

        public IPagedList<SpecificationAttribute> GetPageList(int pageIndex, int pageSize)
        {
            var query = _SpecificationAttributeRepository.Table;
            query = query.OrderByDescending(a => a.Id);
            var result = new PagedList<SpecificationAttribute>(query, pageIndex, pageSize);
            return result;
        }

        public async Task<int> InsertAsync(SpecificationAttribute entity)
        {
            if (entity == null)
                throw new ArgumentNullException("SpecificationAttribute");
            return await _SpecificationAttributeRepository.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(SpecificationAttribute entity)
        {
            if (entity == null)
                throw new ArgumentNullException("SpecificationAttribute");

            return await _SpecificationAttributeRepository.UpdateAsync(entity);
        }
    }
}
