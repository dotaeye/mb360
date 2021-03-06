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
	public class BannerService : IBannerService
    {
		   #region Fields

        private readonly IRepository<Banner> _BannerRepository;

        #endregion

        #region Ctor

        public BannerService(IRepository<Banner> BannerRepository
           )
        {
            this._BannerRepository = BannerRepository;

        }
        #endregion

        public async Task<int> DeleteAsync(Banner entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Banner");

            entity.Deleted = true;
           return await UpdateAsync(entity);
        }

        public async Task<Banner> FindOneAsync(int Id)
        {
            if (Id == 0)
                return null;

            var entity = await _BannerRepository.GetByIdAsync(Id);
            return entity;
        }

        public IQueryable<Banner> GetAll()
        {
            return _BannerRepository.Table;
        }

        public IPagedList<Banner> GetPageList(int pageIndex, int pageSize)
        {
            var query = _BannerRepository.Table;
            query = query.OrderByDescending(a => a.Id);
            var result = new PagedList<Banner>(query, pageIndex, pageSize);
            return result;
        }

        public async Task<int> InsertAsync(Banner entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Banner");
            return await _BannerRepository.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(Banner entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Banner");

            return await _BannerRepository.UpdateAsync(entity);
        }
    }
}
