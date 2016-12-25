import * as orderTypes from '../contants/order';

const initialState = {
  loaded: false,
  entity: {}
};

export default function order(state = initialState, action = {}) {
  switch (action.type) {
    case orderTypes.GET_ALL_ORDER:
    case orderTypes.GET_ONE_ORDER:
    case orderTypes.CREATE_ORDER:
    case orderTypes.UPDATE_ORDER:
    case orderTypes.DELETE_ORDER:
      return {
        ...state,
        loading: true
      };
    case orderTypes.GET_ALL_ORDER_FAIL:
    case orderTypes.GET_ONE_ORDER_FAIL:
    case orderTypes.CREATE_ORDER_FAIL:
    case orderTypes.UPDATE_ORDER_FAIL:
    case orderTypes.DELETE_ORDER_FAIL:
      return {
        ...state,
        loading: false,
        error: action.error
      };
    case orderTypes.GET_ALL_ORDER_SUCCESS:
      return {
        ...state,
        loading: false,
        loaded: true,
        list: action.result
      };
    case orderTypes.GET_ONE_ORDER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case orderTypes.CREATE_ORDER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case orderTypes.UPDATE_ORDER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    case orderTypes.DELETE_ORDER_SUCCESS:
      return {
        ...state,
        loading: false,
        entity: action.result
      };
    default:
      return state;
  }
}

