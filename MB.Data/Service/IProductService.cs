﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using SQ.Core.Data;
using MB.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace MB.Data.Service
{
    public interface IProductService
    {
        Task<int> DeleteAsync(Product entity);

        IPagedList<Product> GetPageList(int pageIndex, int pageSize);

        Task<int> InsertAsync(Product entity);

        Task<int> UpdateAsync(Product entity);

        Task<int> UpdateAsync(IEnumerable<Product> entities);

        IQueryable<Product> GetAll();

        Task<Product> FindOneAsync(int Id);

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="filterableSpecificationAttributeOptionIds">The specification attribute option identifiers applied to loaded products (all pages)</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="manufacturerId">Manufacturer identifier; 0 to load all records</param>
        /// <param name="RoleType">会员类型</param>
        /// <param name="isAgreeActive">是否同意参与活动</param>
        /// <param name="featuredProducts">A value indicating whether loaded products are marked as featured (relates only to categories and manufacturers). 0 to load featured products only, 1 to load not featured products only, null to load all products</param>
        /// <param name="priceMin">Minimum price; null to load all records</param>
        /// <param name="priceMax">Maximum price; null to load all records</param>
        /// <param name="keywords">Keywords</param>
        /// <param name="filteredSpecs">Filtered product specification identifiers</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Products</returns>
        IPagedList<Product> SearchProducts(
            out IList<int> filterableSpecificationAttributeOptionIds,
            bool loadFilterableSpecificationAttributeOptionIds,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            IList<int> categoryIds = null,
            IList<int> manufacturerIds = null,
            int carId = 0,
            int RoleId = 0,
            bool album = false,
            bool isAgreeActive = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            string keywords = null,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            DbGeography location = null,
            bool showHidden = false);
    }
}
