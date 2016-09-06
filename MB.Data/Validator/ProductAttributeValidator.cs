﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using FluentValidation;
using MB.Data.DTO;

namespace MB.Data.Validator
{
    public class ProductAttributeValidator : AbstractValidator<ProductAttributeDTO>
    {
        public ProductAttributeValidator()
        {
			RuleFor(ProductAttribute => ProductAttribute.Name).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
