

import * as orderTypes from '../contants/order';

export function getAll(params) {
  return {
    types: [orderTypes.GET_ALL_ORDER, orderTypes.GET_ALL_ORDER_SUCCESS, orderTypes.GET_ALL_ORDER_FAIL],
    promise: (client) => client.get('/order',{
      token: true,
      params
    })
  };
}


export function getById(id) {
  return {
    types: [orderTypes.GET_ONE_ORDER, orderTypes.GET_ONE_ORDER_SUCCESS, orderTypes.GET_ONE_ORDER_FAIL],
    promise: (client) => client.get('/order/'+id,{
      token: true
    })
  };
}

export function create(data) {
  return {
    types: [orderTypes.CREATE_ORDER, orderTypes.CREATE_ORDER_SUCCESS, orderTypes.CREATE_ORDER_FAIL],
    promise: (client) => client.post('/order', {
      data: data,
      token: true
    })
  };
}

export function update(data) {
  return {
    types: [orderTypes.UPDATE_ORDER, orderTypes.UPDATE_ORDER_SUCCESS, orderTypes.UPDATE_ORDER_FAIL],
    promise: (client) => client.put('/order', {
      data: data,
      token: true
    })
  };
}

export function remove(id) {
  return {
    types: [orderTypes.DELETE_ORDER, orderTypes.DELETE_ORDER_SUCCESS, orderTypes.DELETE_ORDER_FAIL],
    promise: (client) => client.del('/order/'+id, {
      token: true
    })
  };
}

