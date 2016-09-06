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
	public class ProductAttributeValueService : IProductAttributeValueService
    {
		   #region Fields

        private readonly IRepository<ProductAttributeValue> _ProductAttributeValueRepository;

        #endregion

        #region Ctor

        public ProductAttributeValueService(IRepository<ProductAttributeValue> ProductAttributeValueRepository
           )
        {
            this._ProductAttributeValueRepository = ProductAttributeValueRepository;

        }
        #endregion

        public async Task<int> DeleteAsync(ProductAttributeValue entity)
        {
            if (entity == null)
                throw new ArgumentNullException("ProductAttributeValue");

            entity.Deleted = true;
           return await UpdateAsync(entity);
        }

        public async Task<ProductAttributeValue> FindOneAsync(int Id)
        {
            if (Id == 0)
                return null;

            var entity = await _ProductAttributeValueRepository.GetByIdAsync(Id);
            return entity;
        }

        public IQueryable<ProductAttributeValue> GetAll()
        {
            return _ProductAttributeValueRepository.Table;
        }

        public IPagedList<ProductAttributeValue> GetPageList(int pageIndex, int pageSize)
        {
            var query = _ProductAttributeValueRepository.Table;
            query = query.OrderByDescending(a => a.Id);
            var result = new PagedList<ProductAttributeValue>(query, pageIndex, pageSize);
            return result;
        }

        public async Task<int> InsertAsync(ProductAttributeValue entity)
        {
            if (entity == null)
                throw new ArgumentNullException("ProductAttributeValue");
            return await _ProductAttributeValueRepository.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(ProductAttributeValue entity)
        {
            if (entity == null)
                throw new ArgumentNullException("ProductAttributeValue");

            return await _ProductAttributeValueRepository.UpdateAsync(entity);
        }
    }
}