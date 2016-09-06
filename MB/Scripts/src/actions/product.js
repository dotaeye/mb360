﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


import * as productTypes from '../contants/product';

export function getAll(params) {
  return {
    types: [productTypes.GET_ALL_PRODUCT, productTypes.GET_ALL_PRODUCT_SUCCESS, productTypes.GET_ALL_PRODUCT_FAIL],
    promise: (client) => client.get('/product',{
      token: true,
      params
    })
  };
}

export function getById(id) {
  return {
    types: [productTypes.GET_ONE_PRODUCT, productTypes.GET_ONE_PRODUCT_SUCCESS, productTypes.GET_ONE_PRODUCT_FAIL],
    promise: (client) => client.get('/product/'+id,{
      token: true
    })
  };
}

export function create(data) {
  return {
    types: [productTypes.CREATE_PRODUCT, productTypes.CREATE_PRODUCT_SUCCESS, productTypes.CREATE_PRODUCT_FAIL],
    promise: (client) => client.post('/product', {
      data: data,
	    token: true
    })
  };
}

export function update(data) {
  return {
    types: [productTypes.UPDATE_PRODUCT, productTypes.UPDATE_PRODUCT_SUCCESS, productTypes.UPDATE_PRODUCT_FAIL],
    promise: (client) => client.put('/product', {
      data: data,
	    token: true
    })
  };
}

export function remove(id) {
  return {
    types: [productTypes.DELETE_PRODUCT, productTypes.DELETE_PRODUCT_SUCCESS, productTypes.DELETE_PRODUCT_FAIL],
    promise: (client) => client.del('/product/'+id, {
		token: true
    })
  };
}


