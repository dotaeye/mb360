﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using FluentValidation;
using MB.Data.DTO;

namespace MB.Data.Validator
{
    public class ProductAttributeMappingValidator : AbstractValidator<ProductAttributeMappingDTO>
    {
        public ProductAttributeMappingValidator()
        {
			RuleFor(ProductAttributeMapping => ProductAttributeMapping.Name).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}

