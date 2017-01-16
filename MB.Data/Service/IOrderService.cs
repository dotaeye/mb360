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
	public interface IOrderService
    {
		Task<int> DeleteAsync(Order entity);

        IPagedList<Order> GetPageList(int pageIndex, int pageSize);

        Task<int> InsertAsync(Order entity);

        void Insert(Order entity);

        Task<int> UpdateAsync(Order entity);

        void Update(Order entity);

        IQueryable<Order> GetAll();

        Task<Order> FindOneAsync(int Id);	
    }
}
