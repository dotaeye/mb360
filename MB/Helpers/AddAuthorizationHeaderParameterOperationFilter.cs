﻿using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Description;
using Swashbuckle.Swagger;
namespace MB.Helpers
{
    public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
            var isAuthorized = filterPipeline
                                             .Select(filterInfo => filterInfo.Instance)
                                             .Any(filter => filter is IAuthorizationFilter);

            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

            if (isAuthorized && !allowAnonymous)
            {
                if (operation.parameters == null) operation.parameters = new List<Parameter>();

                operation.parameters.Add(new Parameter
                {
                    name = "Authorization",
                    @in = "header",
                    description = "access token",
                    required = true,
                    type = "string"
                });
            }
        }
    }
}