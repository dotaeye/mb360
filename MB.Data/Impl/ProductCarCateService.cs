﻿
// <autogenerated>
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
    public class ProductCarCateService : IProductCarCateService
    {
        #region Fields

        private readonly IRepository<ProductCarCate> _ProductCarCateRepository;

        #endregion

        #region Ctor

        public ProductCarCateService(IRepository<ProductCarCate> ProductCarCateRepository
           )
        {
            this._ProductCarCateRepository = ProductCarCateRepository;

        }
        #endregion

        public async Task<int> DeleteAsync(ProductCarCate entity)
        {
            if (entity == null)
                throw new ArgumentNullException("ProductCarCate");

            return await _ProductCarCateRepository.DeleteAsync(entity);
        }

        public async Task<ProductCarCate> FindOneAsync(int Id)
        {
            if (Id == 0)
                return null;

            var entity = await _ProductCarCateRepository.GetByIdAsync(Id);
            return entity;
        }

        public IQueryable<ProductCarCate> GetAll()
        {
            return _ProductCarCateRepository.Table;
        }

        public IPagedList<ProductCarCate> GetPageList(int pageIndex, int pageSize)
        {
            var query = _ProductCarCateRepository.Table;
            query = query.OrderByDescending(a => a.Id);
            var result = new PagedList<ProductCarCate>(query, pageIndex, pageSize);
            return result;
        }

        public async Task<int> InsertAsync(ProductCarCate entity)
        {
            if (entity == null)
                throw new ArgumentNullException("ProductCarCate");
            return await _ProductCarCateRepository.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(ProductCarCate entity)
        {
            if (entity == null)
                throw new ArgumentNullException("ProductCarCate");

            return await _ProductCarCateRepository.UpdateAsync(entity);
        }
    }
}
