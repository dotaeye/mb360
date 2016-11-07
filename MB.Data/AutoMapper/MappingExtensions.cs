﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MB.Data.Models;
using MB.Data.DTO;

namespace MB.Data.AutoMapper
{
    public static class MappingExtensions
    {

        public static CarCateDTO ToModel(this CarCate entity)
        {
            return Mapper.Map<CarCate, CarCateDTO>(entity);
        }

        public static CarCate ToEntity(this CarCateDTO dto)
        {
            return Mapper.Map<CarCateDTO, CarCate>(dto);
        }

        public static CarCate ToEntity(this CarCateDTO dto, CarCate entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static CityCateDTO ToModel(this CityCate entity)
        {
            return Mapper.Map<CityCate, CityCateDTO>(entity);
        }

        public static CityCate ToEntity(this CityCateDTO dto)
        {
            return Mapper.Map<CityCateDTO, CityCate>(dto);
        }

        public static CityCate ToEntity(this CityCateDTO dto, CityCate entity)
        {
            return Mapper.Map(dto, entity);
        }



        public static CategoryDTO ToModel(this Category entity)
        {
            return Mapper.Map<Category, CategoryDTO>(entity);
        }

        public static Category ToEntity(this CategoryDTO dto)
        {
            return Mapper.Map<CategoryDTO, Category>(dto);
        }

        public static Category ToEntity(this CategoryDTO dto, Category entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static DepartmentDTO ToModel(this Department entity)
        {
            return Mapper.Map<Department, DepartmentDTO>(entity);
        }

        public static Department ToEntity(this DepartmentDTO dto)
        {
            return Mapper.Map<DepartmentDTO, Department>(dto);
        }

        public static Department ToEntity(this DepartmentDTO dto, Department entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static JobDTO ToModel(this Job entity)
        {
            return Mapper.Map<Job, JobDTO>(entity);
        }

        public static Job ToEntity(this JobDTO dto)
        {
            return Mapper.Map<JobDTO, Job>(dto);
        }

        public static Job ToEntity(this JobDTO dto, Job entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static ManufacturerDTO ToModel(this Manufacturer entity)
        {
            return Mapper.Map<Manufacturer, ManufacturerDTO>(entity);
        }

        public static Manufacturer ToEntity(this ManufacturerDTO dto)
        {
            return Mapper.Map<ManufacturerDTO, Manufacturer>(dto);
        }

        public static Manufacturer ToEntity(this ManufacturerDTO dto, Manufacturer entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static ProductDTO ToModel(this Product entity)
        {
            return Mapper.Map<Product, ProductDTO>(entity);
        }

        public static Product ToEntity(this ProductDTO dto)
        {
            return Mapper.Map<ProductDTO, Product>(dto);
        }

        public static Product ToEntity(this ProductDTO dto, Product entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static ProductAttributeDTO ToModel(this ProductAttribute entity)
        {
            return Mapper.Map<ProductAttribute, ProductAttributeDTO>(entity);
        }

        public static ProductAttribute ToEntity(this ProductAttributeDTO dto)
        {
            return Mapper.Map<ProductAttributeDTO, ProductAttribute>(dto);
        }

        public static ProductAttribute ToEntity(this ProductAttributeDTO dto, ProductAttribute entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static ProductAttributeMappingDTO ToModel(this ProductAttributeMapping entity)
        {
            return Mapper.Map<ProductAttributeMapping, ProductAttributeMappingDTO>(entity);
        }

        public static ProductAttributeMapping ToEntity(this ProductAttributeMappingDTO dto)
        {
            return Mapper.Map<ProductAttributeMappingDTO, ProductAttributeMapping>(dto);
        }

        public static ProductAttributeMapping ToEntity(this ProductAttributeMappingDTO dto, ProductAttributeMapping entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static ProductAttributeValueDTO ToModel(this ProductAttributeValue entity)
        {
            return Mapper.Map<ProductAttributeValue, ProductAttributeValueDTO>(entity);
        }

        public static ProductAttributeValue ToEntity(this ProductAttributeValueDTO dto)
        {
            return Mapper.Map<ProductAttributeValueDTO, ProductAttributeValue>(dto);
        }

        public static ProductAttributeValue ToEntity(this ProductAttributeValueDTO dto, ProductAttributeValue entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static ProductManufacturerDTO ToModel(this ProductManufacturer entity)
        {
            return Mapper.Map<ProductManufacturer, ProductManufacturerDTO>(entity);
        }

        public static ProductManufacturer ToEntity(this ProductManufacturerDTO dto)
        {
            return Mapper.Map<ProductManufacturerDTO, ProductManufacturer>(dto);
        }

        public static ProductManufacturer ToEntity(this ProductManufacturerDTO dto, ProductManufacturer entity)
        {
            return Mapper.Map(dto, entity);
        }


        public static ProductCarCateDTO ToModel(this ProductCarCate entity)
        {
            return Mapper.Map<ProductCarCate, ProductCarCateDTO>(entity);
        }

        public static ProductCarCate ToEntity(this ProductCarCateDTO dto)
        {
            return Mapper.Map<ProductCarCateDTO, ProductCarCate>(dto);
        }

        public static ProductCarCate ToEntity(this ProductCarCateDTO dto, ProductCarCate entity)
        {
            return Mapper.Map(dto, entity);
        }


        public static ProductSpecificationAttributeDTO ToModel(this ProductSpecificationAttribute entity)
        {
            return Mapper.Map<ProductSpecificationAttribute, ProductSpecificationAttributeDTO>(entity);
        }

        public static ProductSpecificationAttribute ToEntity(this ProductSpecificationAttributeDTO dto)
        {
            return Mapper.Map<ProductSpecificationAttributeDTO, ProductSpecificationAttribute>(dto);
        }

        public static ProductSpecificationAttribute ToEntity(this ProductSpecificationAttributeDTO dto, ProductSpecificationAttribute entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static ProductStorageQuantityDTO ToModel(this ProductStorageQuantity entity)
        {
            return Mapper.Map<ProductStorageQuantity, ProductStorageQuantityDTO>(entity);
        }

        public static ProductStorageQuantity ToEntity(this ProductStorageQuantityDTO dto)
        {
            return Mapper.Map<ProductStorageQuantityDTO, ProductStorageQuantity>(dto);
        }

        public static ProductStorageQuantity ToEntity(this ProductStorageQuantityDTO dto, ProductStorageQuantity entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static SpecificationAttributeDTO ToModel(this SpecificationAttribute entity)
        {
            return Mapper.Map<SpecificationAttribute, SpecificationAttributeDTO>(entity);
        }

        public static SpecificationAttribute ToEntity(this SpecificationAttributeDTO dto)
        {
            return Mapper.Map<SpecificationAttributeDTO, SpecificationAttribute>(dto);
        }

        public static SpecificationAttribute ToEntity(this SpecificationAttributeDTO dto, SpecificationAttribute entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static SpecificationAttributeOptionDTO ToModel(this SpecificationAttributeOption entity)
        {
            return Mapper.Map<SpecificationAttributeOption, SpecificationAttributeOptionDTO>(entity);
        }

        public static SpecificationAttributeOption ToEntity(this SpecificationAttributeOptionDTO dto)
        {
            return Mapper.Map<SpecificationAttributeOptionDTO, SpecificationAttributeOption>(dto);
        }

        public static SpecificationAttributeOption ToEntity(this SpecificationAttributeOptionDTO dto, SpecificationAttributeOption entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static StorageDTO ToModel(this Storage entity)
        {
            return Mapper.Map<Storage, StorageDTO>(entity);
        }

        public static Storage ToEntity(this StorageDTO dto)
        {
            return Mapper.Map<StorageDTO, Storage>(dto);
        }

        public static Storage ToEntity(this StorageDTO dto, Storage entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static UserActivitiesDTO ToModel(this UserActivities entity)
        {
            return Mapper.Map<UserActivities, UserActivitiesDTO>(entity);
        }

        public static UserActivities ToEntity(this UserActivitiesDTO dto)
        {
            return Mapper.Map<UserActivitiesDTO, UserActivities>(dto);
        }

        public static UserActivities ToEntity(this UserActivitiesDTO dto, UserActivities entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static UserPermissionDTO ToModel(this UserPermission entity)
        {
            return Mapper.Map<UserPermission, UserPermissionDTO>(entity);
        }

        public static UserPermission ToEntity(this UserPermissionDTO dto)
        {
            return Mapper.Map<UserPermissionDTO, UserPermission>(dto);
        }

        public static UserPermission ToEntity(this UserPermissionDTO dto, UserPermission entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static UserRoleDTO ToModel(this UserRole entity)
        {
            return Mapper.Map<UserRole, UserRoleDTO>(entity);
        }

        public static UserRole ToEntity(this UserRoleDTO dto)
        {
            return Mapper.Map<UserRoleDTO, UserRole>(dto);
        }

        public static UserRole ToEntity(this UserRoleDTO dto, UserRole entity)
        {
            return Mapper.Map(dto, entity);
        }



        public static MemberDTO ToModel(this ApplicationUser entity)
        {
            return Mapper.Map<ApplicationUser, MemberDTO>(entity);
        }

        public static ApplicationUser ToEntity(this MemberDTO dto)
        {
            return Mapper.Map<MemberDTO, ApplicationUser>(dto);
        }

        public static ApplicationUser ToEntity(this MemberDTO dto, ApplicationUser entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static SpecificationAttributeCategoryMappingDTO ToModel(this SpecificationAttributeCategoryMapping entity)
        {
            return Mapper.Map<SpecificationAttributeCategoryMapping, SpecificationAttributeCategoryMappingDTO>(entity);
        }

        public static SpecificationAttributeCategoryMapping ToEntity(this SpecificationAttributeCategoryMappingDTO dto)
        {
            return Mapper.Map<SpecificationAttributeCategoryMappingDTO, SpecificationAttributeCategoryMapping>(dto);
        }

        public static SpecificationAttributeCategoryMapping ToEntity(this SpecificationAttributeCategoryMappingDTO dto, SpecificationAttributeCategoryMapping entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static ProductAttributeCategoryMappingDTO ToModel(this ProductAttributeCategoryMapping entity)
        {
            return Mapper.Map<ProductAttributeCategoryMapping, ProductAttributeCategoryMappingDTO>(entity);
        }

        public static ProductAttributeCategoryMapping ToEntity(this ProductAttributeCategoryMappingDTO dto)
        {
            return Mapper.Map<ProductAttributeCategoryMappingDTO, ProductAttributeCategoryMapping>(dto);
        }

        public static ProductAttributeCategoryMapping ToEntity(this ProductAttributeCategoryMappingDTO dto, ProductAttributeCategoryMapping entity)
        {
            return Mapper.Map(dto, entity);
        }


        public static ShoppingCartItemDTO ToModel(this ShoppingCartItem entity)
        {
            return Mapper.Map<ShoppingCartItem, ShoppingCartItemDTO>(entity);
        }

        public static ShoppingCartItem ToEntity(this ShoppingCartItemDTO dto)
        {
            return Mapper.Map<ShoppingCartItemDTO, ShoppingCartItem>(dto);
        }

        public static ShoppingCartItem ToEntity(this ShoppingCartItemDTO dto, ShoppingCartItem entity)
        {
            return Mapper.Map(dto, entity);
        }
        public static OrderDTO ToModel(this Order entity)
        {
            return Mapper.Map<Order, OrderDTO>(entity);
        }

        public static Order ToEntity(this OrderDTO dto)
        {
            return Mapper.Map<OrderDTO, Order>(dto);
        }

        public static Order ToEntity(this OrderDTO dto, Order entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static OrderItemDTO ToModel(this OrderItem entity)
        {
            return Mapper.Map<OrderItem, OrderItemDTO>(entity);
        }

        public static OrderItem ToEntity(this OrderItemDTO dto)
        {
            return Mapper.Map<OrderItemDTO, OrderItem>(dto);
        }

        public static OrderItem ToEntity(this OrderItemDTO dto, OrderItem entity)
        {
            return Mapper.Map(dto, entity);
        }


        public static AddressDTO ToModel(this Address entity)
        {
            return Mapper.Map<Address, AddressDTO>(entity);
        }

        public static Address ToEntity(this AddressDTO dto)
        {
            return Mapper.Map<AddressDTO, Address>(dto);
        }

        public static Address ToEntity(this AddressDTO dto, Address entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static BannerDTO ToModel(this Banner entity)
        {
            return Mapper.Map<Banner, BannerDTO>(entity);
        }

        public static Banner ToEntity(this BannerDTO dto)
        {
            return Mapper.Map<BannerDTO, Banner>(dto);
        }

        public static Banner ToEntity(this BannerDTO dto, Banner entity)
        {
            return Mapper.Map(dto, entity);
        }

    }
}
