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
	public interface ISpecificationAttributeCategoryMappingService
    {
		Task<int> DeleteAsync(SpecificationAttributeCategoryMapping entity);

        IPagedList<SpecificationAttributeCategoryMapping> GetPageList(int pageIndex, int pageSize);

        Task<int> InsertAsync(SpecificationAttributeCategoryMapping entity);

        Task<int> UpdateAsync(SpecificationAttributeCategoryMapping entity);

        IQueryable<SpecificationAttributeCategoryMapping> GetAll();

        Task<SpecificationAttributeCategoryMapping> FindOneAsync(int Id);	
    }
}
