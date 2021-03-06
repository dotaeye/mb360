﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using SQ.Core.Data;
using MB.Data.Models;
using System.Threading.Tasks;

namespace MB.Data.Service
{
	public interface IProductStorageQuantityService
    {
		Task<int> DeleteAsync(ProductStorageQuantity entity);

        IPagedList<ProductStorageQuantity> GetPageList(int pageIndex, int pageSize);

        Task<int> InsertAsync(ProductStorageQuantity entity);

        Task<int> UpdateAsync(ProductStorageQuantity entity);

        IQueryable<ProductStorageQuantity> GetAll();

        Task<ProductStorageQuantity> FindOneAsync(int Id);	
    }
}
