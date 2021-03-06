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
	public class SmsCodeService : ISmsCodeService
    {
		   #region Fields

        private readonly IRepository<SmsCode> _SmsCodeRepository;

        #endregion

        #region Ctor

        public SmsCodeService(IRepository<SmsCode> SmsCodeRepository
           )
        {
            this._SmsCodeRepository = SmsCodeRepository;

        }
        #endregion

        public async Task<int> DeleteAsync(SmsCode entity)
        {
            if (entity == null)
                throw new ArgumentNullException("SmsCode");

            //entity.Deleted = true;
           return await _SmsCodeRepository.DeleteAsync(entity);
        }

        public async Task<SmsCode> FindOneAsync(int Id)
        {
            if (Id == 0)
                return null;

            var entity = await _SmsCodeRepository.GetByIdAsync(Id);
            return entity;
        }

        public IQueryable<SmsCode> GetAll()
        {
            return _SmsCodeRepository.Table;
        }

        public IPagedList<SmsCode> GetPageList(int pageIndex, int pageSize)
        {
            var query = _SmsCodeRepository.Table;
            query = query.OrderByDescending(a => a.Id);
            var result = new PagedList<SmsCode>(query, pageIndex, pageSize);
            return result;
        }

        public async Task<int> InsertAsync(SmsCode entity)
        {
            if (entity == null)
                throw new ArgumentNullException("SmsCode");
            return await _SmsCodeRepository.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(SmsCode entity)
        {
            if (entity == null)
                throw new ArgumentNullException("SmsCode");

            return await _SmsCodeRepository.UpdateAsync(entity);
        }
    }
}
