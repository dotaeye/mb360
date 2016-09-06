﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>



import * as productStorageQuantityTypes from '../contants/productStorageQuantity';

const initialState = {
  loaded: false,
  entity: {}
};

export default function productStorageQuantity(state = initialState, action = {}) {
  switch (action.type) {
    case productStorageQuantityTypes.GET_ALL_PRODUCTSTORAGEQUANTITY:
    case productStorageQuantityTypes.GET_ONE_PRODUCTSTORAGEQUANTITY:
    case productStorageQuantityTypes.CREATE_PRODUCTSTORAGEQUANTITY:
    case productStorageQuantityTypes.UPDATE_PRODUCTSTORAGEQUANTITY:
    case productStorageQuantityTypes.DELETE_PRODUCTSTORAGEQUANTITY:
      return {
        ...state,
        loading: true
      };
    case productStorageQuantityTypes.GET_ALL_PRODUCTSTORAGEQUANTITY_FAIL:
    case productStorageQuantityTypes.GET_ONE_PRODUCTSTORAGEQUANTITY_FAIL:
    case productStorageQuantityTypes.CREATE_PRODUCTSTORAGEQUANTITY_FAIL:
    case productStorageQuantityTypes.UPDATE_PRODUCTSTORAGEQUANTITY_FAIL:
    case productStorageQuantityTypes.DELETE_PRODUCTSTORAGEQUANTITY_FAIL:
      return {
        ...state,
        loading: false,
        error: action.error
      };
    case productStorageQuantityTypes.GET_ALL_PRODUCTSTORAGEQUANTITY_SUCCESS:
      return {
        ...state,
        loading: false,
        loaded: true,
        list: action.result
      };
    case productStorageQuantityTypes.GET_ONE_PRODUCTSTORAGEQUANTITY_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case productStorageQuantityTypes.CREATE_PRODUCTSTORAGEQUANTITY_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case productStorageQuantityTypes.UPDATE_PRODUCTSTORAGEQUANTITY_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case productStorageQuantityTypes.DELETE_PRODUCTSTORAGEQUANTITY_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    default:
      return state;
  }
}

