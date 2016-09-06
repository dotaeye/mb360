﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>



import * as productAttributeTypes from '../contants/productAttribute';

const initialState = {
  loaded: false,
  entity: {}
};

export default function productAttribute(state = initialState, action = {}) {
  switch (action.type) {
    case productAttributeTypes.GET_ALL_PRODUCTATTRIBUTE:
    case productAttributeTypes.GET_ONE_PRODUCTATTRIBUTE:
    case productAttributeTypes.CREATE_PRODUCTATTRIBUTE:
    case productAttributeTypes.UPDATE_PRODUCTATTRIBUTE:
    case productAttributeTypes.DELETE_PRODUCTATTRIBUTE:
      return {
        ...state,
        loading: true
      };
    case productAttributeTypes.GET_ALL_PRODUCTATTRIBUTE_FAIL:
    case productAttributeTypes.GET_ONE_PRODUCTATTRIBUTE_FAIL:
    case productAttributeTypes.CREATE_PRODUCTATTRIBUTE_FAIL:
    case productAttributeTypes.UPDATE_PRODUCTATTRIBUTE_FAIL:
    case productAttributeTypes.DELETE_PRODUCTATTRIBUTE_FAIL:
      return {
        ...state,
        loading: false,
        error: action.error
      };
    case productAttributeTypes.GET_ALL_PRODUCTATTRIBUTE_SUCCESS:
      return {
        ...state,
        loading: false,
        loaded: true,
        list: action.result
      };
    case productAttributeTypes.GET_ONE_PRODUCTATTRIBUTE_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case productAttributeTypes.CREATE_PRODUCTATTRIBUTE_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case productAttributeTypes.UPDATE_PRODUCTATTRIBUTE_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case productAttributeTypes.DELETE_PRODUCTATTRIBUTE_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    default:
      return state;
  }
}
