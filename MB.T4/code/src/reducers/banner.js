﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>



import * as bannerTypes from '../contants/banner';

const initialState = {
  loaded: false,
  entity: {}
};

export default function banner(state = initialState, action = {}) {
  switch (action.type) {
    case bannerTypes.GET_ALL_BANNER:
    case bannerTypes.GET_ONE_BANNER:
    case bannerTypes.CREATE_BANNER:
    case bannerTypes.UPDATE_BANNER:
    case bannerTypes.DELETE_BANNER:
      return {
        ...state,
        loading: true
      };
    case bannerTypes.GET_ALL_BANNER_FAIL:
    case bannerTypes.GET_ONE_BANNER_FAIL:
    case bannerTypes.CREATE_BANNER_FAIL:
    case bannerTypes.UPDATE_BANNER_FAIL:
    case bannerTypes.DELETE_BANNER_FAIL:
      return {
        ...state,
        loading: false,
        error: action.error
      };
    case bannerTypes.GET_ALL_BANNER_SUCCESS:
      return {
        ...state,
        loading: false,
        loaded: true,
        list: action.result
      };
    case bannerTypes.GET_ONE_BANNER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case bannerTypes.CREATE_BANNER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case bannerTypes.UPDATE_BANNER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case bannerTypes.DELETE_BANNER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    default:
      return state;
  }
}

