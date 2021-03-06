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
	public class UserPermissionService : IUserPermissionService
    {
		   #region Fields

        private readonly IRepository<UserPermission> _UserPermissionRepository;

        #endregion

        #region Ctor

        public UserPermissionService(IRepository<UserPermission> UserPermissionRepository
           )
        {
            this._UserPermissionRepository = UserPermissionRepository;

        }
        #endregion

        public async Task<int> DeleteAsync(UserPermission entity)
        {
            if (entity == null)
                throw new ArgumentNullException("UserPermission");

            entity.Deleted = true;
           return await UpdateAsync(entity);
        }

        public async Task<UserPermission> FindOneAsync(int Id)
        {
            if (Id == 0)
                return null;

            var entity = await _UserPermissionRepository.GetByIdAsync(Id);
            return entity;
        }

        public IQueryable<UserPermission> GetAll()
        {
            return _UserPermissionRepository.Table;
        }

        public IPagedList<UserPermission> GetPageList(int pageIndex, int pageSize)
        {
            var query = _UserPermissionRepository.Table;
            query = query.OrderByDescending(a => a.Id);
            var result = new PagedList<UserPermission>(query, pageIndex, pageSize);
            return result;
        }

        public async Task<int> InsertAsync(UserPermission entity)
        {
            if (entity == null)
                throw new ArgumentNullException("UserPermission");
            return await _UserPermissionRepository.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(UserPermission entity)
        {
            if (entity == null)
                throw new ArgumentNullException("UserPermission");

            return await _UserPermissionRepository.UpdateAsync(entity);
        }
    }
}
