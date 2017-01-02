import * as orderTypes from '../contants/order';

export function getAll(params) {
  return {
    types: [orderTypes.GET_ALL_ORDER, orderTypes.GET_ALL_ORDER_SUCCESS, orderTypes.GET_ALL_ORDER_FAIL],
    promise: (client) => client.get('/order', {
      token: true,
      params
    })
  };
}


export function getById(id) {
  return {
    types: [orderTypes.GET_ONE_ORDER, orderTypes.GET_ONE_ORDER_SUCCESS, orderTypes.GET_ONE_ORDER_FAIL],
    promise: (client) => client.get('/order/' + id, {
      token: true
    })
  };
}


export function updateStatus(data) {
  return {
    types: [orderTypes.UPDATE_ORDER_STATUS, orderTypes.UPDATE_ORDER_STATUS_SUCCESS, orderTypes.UPDATE_ORDER_STATUS_FAIL],
    promise: (client) => client.put('/order', {
      data: data,
      token: true
    }),
    payload: data
  };
}


