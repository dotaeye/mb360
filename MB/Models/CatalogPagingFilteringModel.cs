using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using SQ.Core.Data;
using SQ.Core.Caching;
using MB.Helpers;
using MB.Data.Service;
using MB.Data.Models;
using System.Globalization;

namespace MB.Models
{
    public class CatalogPagingFilteringModel : BasePageableModel
    {
        public CatalogPagingFilteringModel()
        {
            this.AvailableSortOptions = new List<SelectListItem>();
            this.FilteredItems = new List<SpecificationFilter>();
        }

        public int Id { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string Specs { get; set; }

        public IList<SpecificationFilter> FilteredItems { get; set; }
        /// <summary>
        /// Available sort options
        /// </summary>
        public IList<SelectListItem> AvailableSortOptions { get; set; }

        /// <summary>
        /// Order by
        /// </summary>
        public int? OrderBy { get; set; }


        public virtual List<int> GetAlreadyFilteredSpecOptionIds()
        {
            var result = new List<int>();

            var alreadyFilteredSpecsStr = MBHelper.QueryString<string>("specs");
            if (String.IsNullOrWhiteSpace(alreadyFilteredSpecsStr))
                return result;

            foreach (var spec in alreadyFilteredSpecsStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int specId;
                int.TryParse(spec.Trim(), out specId);
                if (!result.Contains(specId))
                    result.Add(specId);
            }
            return result;
        }

        /// <summary>
        /// Prepare model
        /// </summary>
        /// <param name="alreadyFilteredSpecOptionIds">IDs of already filtered specification options</param>
        /// <param name="filterableSpecificationAttributeOptionIds">IDs of filterable specification options</param>
        /// <param name="specificationAttributeService"></param>
        /// <param name="webHelper">Web helper</param>
        /// <param name="workContext">Work context</param>
        /// <param name="cacheManager">Cache manager</param>
        public virtual void PrepareSpecsFilters(IList<int> alreadyFilteredSpecOptionIds,
            int[] filterableSpecificationAttributeOptionIds,
            ISpecificationAttributeOptionService specificationAttributeOptionService,
            ICacheManager cacheManager)
        {
      
            var optionIds = filterableSpecificationAttributeOptionIds != null
                ? string.Join(",", filterableSpecificationAttributeOptionIds) : string.Empty;
            var cacheKey = string.Format(MBHelper.SPECS_FILTER_MODEL_KEY, optionIds);



            var allOptions = specificationAttributeOptionService.GetAll()
                .Include(x => x.SpecificationAttribute)
                .Where(x => filterableSpecificationAttributeOptionIds.Contains(x.Id));


            var allFilters = allOptions.Select(sao =>
                 new SpecificationAttributeOptionFilter
                 {
                     SpecificationAttributeId = sao.SpecificationAttribute.Id,
                     SpecificationAttributeName = sao.SpecificationAttribute.Name,
                     SpecificationAttributeDisplayOrder = sao.SpecificationAttribute.DisplayOrder,
                     SpecificationAttributeOptionId = sao.Id,
                     SpecificationAttributeOptionName = sao.Name,
                     SpecificationAttributeOptionDisplayOrder = sao.DisplayOrder

                 }).ToList();

            if (!allFilters.Any())
                return;



            //sort loaded options
            allFilters = allFilters.OrderBy(saof => saof.SpecificationAttributeDisplayOrder)
                .ThenBy(saof => saof.SpecificationAttributeName)
                .ThenBy(saof => saof.SpecificationAttributeOptionDisplayOrder)
                .ThenBy(saof => saof.SpecificationAttributeOptionName).ToList();


            foreach (var filter in allFilters)
            {
                if (!this.FilteredItems.Any(x => x.SpecificationAttributeId == filter.SpecificationAttributeId))
                {
                    var specFilter = new SpecificationFilter()
                    {
                        SpecificationAttributeId = filter.SpecificationAttributeId,
                        SpecificationAttributeName = filter.SpecificationAttributeName,
                        DisplayOrder = filter.SpecificationAttributeDisplayOrder
                    };
                    foreach (var filterOption in allFilters.Where(x => x.SpecificationAttributeId == filter.SpecificationAttributeId))
                    {
                        if (!specFilter.SpecificationOptions.Any(x => x.SpecificationAttributeOptionId == filterOption.SpecificationAttributeOptionId))
                        {
                            specFilter.SpecificationOptions.Add(new SpecificationOptionItem()
                            {
                                SpecificationAttributeOptionId = filterOption.SpecificationAttributeOptionId,
                                DisplayOrder = filterOption.SpecificationAttributeOptionDisplayOrder,
                                Checked = alreadyFilteredSpecOptionIds.Contains(filterOption.SpecificationAttributeOptionId),
                                SpecificationAttributeOptionName = filterOption.SpecificationAttributeOptionName
                            });
                        }
                    }
                    this.FilteredItems.Add(specFilter);
                }
            }

        }


     

        /// <summary>
        /// Specification filter item
        /// </summary>
        public partial class SpecificationOptionItem
        {

            public int SpecificationAttributeOptionId { get; set; }

            /// <summary>
            /// Specification attribute option name
            /// </summary>
            public string SpecificationAttributeOptionName { get; set; }

            public bool Checked { get; set; }


            public int DisplayOrder { get; set; }

        }

        public class SpecificationFilter
        {

            public SpecificationFilter()
            {
                this.SpecificationOptions = new List<SpecificationOptionItem>();
            }


            public IList<SpecificationOptionItem> SpecificationOptions { get; set; }
            /// <summary>
            /// Gets or sets the specification attribute identifier
            /// </summary>
            public int SpecificationAttributeId { get; set; }

            /// <summary>
            /// Gets or sets the specification attribute name
            /// </summary>
            public string SpecificationAttributeName { get; set; }

            public int DisplayOrder { get; set; }

        }

        public class SpecificationAttributeOptionFilter
        {
            /// <summary>
            /// Gets or sets the specification attribute identifier
            /// </summary>
            public int SpecificationAttributeId { get; set; }

            /// <summary>
            /// Gets or sets the specification attribute name
            /// </summary>
            public string SpecificationAttributeName { get; set; }

            /// <summary>
            /// Gets or sets the specification attribute display order
            /// </summary>
            public int SpecificationAttributeDisplayOrder { get; set; }

            /// <summary>
            /// Gets or sets the specification attribute option identifier
            /// </summary>
            public int SpecificationAttributeOptionId { get; set; }

            /// <summary>
            /// Gets or sets the specification attribute option name
            /// </summary>
            public string SpecificationAttributeOptionName { get; set; }

            /// <summary>
            /// Gets or sets the specification attribute option display order
            /// </summary>
            public int SpecificationAttributeOptionDisplayOrder { get; set; }
        }
    }
}