﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>



import * as jobTypes from '../contants/job';

const initialState = {
  loaded: false,
  entity: {}
};

export default function job(state = initialState, action = {}) {
  switch (action.type) {
    case jobTypes.GET_ALL_JOB:
    case jobTypes.GET_ONE_JOB:
    case jobTypes.CREATE_JOB:
    case jobTypes.UPDATE_JOB:
    case jobTypes.DELETE_JOB:
      return {
        ...state,
        loading: true
      };
    case jobTypes.GET_ALL_JOB_FAIL:
    case jobTypes.GET_ONE_JOB_FAIL:
    case jobTypes.CREATE_JOB_FAIL:
    case jobTypes.UPDATE_JOB_FAIL:
    case jobTypes.DELETE_JOB_FAIL:
      return {
        ...state,
        loading: false,
        error: action.error
      };
    case jobTypes.GET_ALL_JOB_SUCCESS:
      return {
        ...state,
        loading: false,
        loaded: true,
        list: action.result
      };
    case jobTypes.GET_ONE_JOB_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case jobTypes.CREATE_JOB_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case jobTypes.UPDATE_JOB_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case jobTypes.DELETE_JOB_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    default:
      return state;
  }
}

