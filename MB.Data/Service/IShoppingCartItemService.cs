﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using SQ.Core.Data;
using MB.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MB.Data.Service
{
	public interface IShoppingCartItemService
    {
		Task<int> DeleteAsync(ShoppingCartItem entity);

        IPagedList<ShoppingCartItem> GetPageList(int pageIndex, int pageSize);

        Task<int> InsertAsync(ShoppingCartItem entity);

        Task<int> UpdateAsync(ShoppingCartItem entity);

        void Update(IEnumerable<ShoppingCartItem> entities);

        IQueryable<ShoppingCartItem> GetAll();

        Task<ShoppingCartItem> FindOneAsync(int Id);	
    }
}
