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
	public interface IProductService
    {
		Task<int> DeleteAsync(Product entity);

        IPagedList<Product> GetPageList(int pageIndex, int pageSize);

        Task<int> InsertAsync(Product entity);

        Task<int> UpdateAsync(Product entity);

        IQueryable<Product> GetAll();

        Task<Product> FindOneAsync(int Id);	
    }
}
