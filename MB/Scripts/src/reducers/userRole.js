﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

import * as userRoleTypes from '../contants/userRole';

const initialState = {
  entity: {},
  selectlist:[]
};

export default function userRole(state = initialState, action = {}) {
  switch (action.type) {
    case userRoleTypes.GET_ALL_USERROLE:
    case userRoleTypes.GET_ONE_USERROLE:
    case userRoleTypes.CREATE_USERROLE:
    case userRoleTypes.GET_USERROLE_SELECTLIST:
    case userRoleTypes.UPDATE_USERROLE:
    case userRoleTypes.DELETE_USERROLE:
      return {
        ...state,
        loading: true
      };
    case userRoleTypes.GET_ALL_USERROLE_FAIL:
    case userRoleTypes.GET_ONE_USERROLE_FAIL:
    case userRoleTypes.CREATE_USERROLE_FAIL:
    case userRoleTypes.UPDATE_USERROLE_FAIL:
    case userRoleTypes.DELETE_USERROLE_FAIL:
    case userRoleTypes.GET_USERROLE_SELECTLIST_FAIL:
      return {
        ...state,
        loading: false,
        error: action.error
      };
    case userRoleTypes.GET_ALL_USERROLE_SUCCESS:
      return {
        ...state,
        loading: false,
        list: action.result
      };
    case userRoleTypes.GET_ONE_USERROLE_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case userRoleTypes.CREATE_USERROLE_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case userRoleTypes.UPDATE_USERROLE_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case userRoleTypes.GET_USERROLE_SELECTLIST_SUCCESS:
      return {
        ...state,
        loading: false,
        loaded: true,
        selectlist: action.result
      };

    case userRoleTypes.DELETE_USERROLE_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    default:
      return state;
  }
}

