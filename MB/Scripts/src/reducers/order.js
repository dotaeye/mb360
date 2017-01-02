import * as orderTypes from '../contants/order';

const initialState = {
  loaded: false,
  entity: {}
};

export default function order(state = initialState, action = {}) {
  switch (action.type) {
    case orderTypes.GET_ALL_ORDER:
    case orderTypes.GET_ONE_ORDER:
    case orderTypes.UPDATE_ORDER_STATUS:
      return {
        ...state,
        loading: true
      };
    case orderTypes.GET_ALL_ORDER_FAIL:
    case orderTypes.GET_ONE_ORDER_FAIL:
    case orderTypes.UPDATE_ORDER_STATUS_FAIL:
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
    case orderTypes.UPDATE_ORDER_STATUS_SUCCESS:
      return {
        ...state,
        loading: false,
        list: updateListStatus(state, action)
      };
    default:
      return state;
  }
}


function updateListStatus(state, action) {
  const {id, orderStatusId }= action.payload;
  state.list.data.find(x=>x.id == id).orderStatusId = orderStatusId;
  return state.list;
}
