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
	public interface IProductAttributeCategoryMappingService
    {
		Task<int> DeleteAsync(ProductAttributeCategoryMapping entity);

        IPagedList<ProductAttributeCategoryMapping> GetPageList(int pageIndex, int pageSize);

        Task<int> InsertAsync(ProductAttributeCategoryMapping entity);

        Task<int> UpdateAsync(ProductAttributeCategoryMapping entity);

        IQueryable<ProductAttributeCategoryMapping> GetAll();

        Task<ProductAttributeCategoryMapping> FindOneAsync(int Id);	
    }
}
