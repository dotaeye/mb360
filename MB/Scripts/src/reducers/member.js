
import * as memberTypes from '../contants/member';

const initialState = {
  loaded: false,
  entity: {}
};

export default function member(state = initialState, action = {}) {
  switch (action.type) {
    case memberTypes.GET_ALL_MEMBER:
    case memberTypes.GET_ONE_MEMBER:
    case memberTypes.CREATE_MEMBER:
    case memberTypes.UPDATE_MEMBER:
    case memberTypes.DELETE_MEMBER:
      return {
        ...state,
        loading: true
      };
    case memberTypes.GET_ALL_MEMBER_FAIL:
    case memberTypes.GET_ONE_MEMBER_FAIL:
    case memberTypes.CREATE_MEMBER_FAIL:
    case memberTypes.UPDATE_MEMBER_FAIL:
    case memberTypes.DELETE_MEMBER_FAIL:
      return {
        ...state,
        loading: false,
        error: action.error
      };
    case memberTypes.GET_ALL_MEMBER_SUCCESS:
      return {
        ...state,
        loading: false,
        loaded: true,
        list: action.result
      };
    case memberTypes.GET_ONE_MEMBER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case memberTypes.CREATE_MEMBER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case memberTypes.UPDATE_MEMBER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case memberTypes.DELETE_MEMBER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    default:
      return state;
  }
}

