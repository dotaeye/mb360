import * as productCarCateTypes from '../contants/productCarCate';

export function getAll(params) {
  return {
    types: [productCarCateTypes.GET_ALL_PRODUCTCARCATE, productCarCateTypes.GET_ALL_PRODUCTCARCATE_SUCCESS, productCarCateTypes.GET_ALL_PRODUCTCARCATE_FAIL],
    promise: (client) => client.get('/productCarCate', {
      token: true,
      params
    })
  };
}

export function create(data) {
  return {
    types: [productCarCateTypes.CREATE_PRODUCTCARCATE, productCarCateTypes.CREATE_PRODUCTCARCATE_SUCCESS, productCarCateTypes.CREATE_PRODUCTCARCATE_FAIL],
    promise: (client) => client.post('/productCarCate', {
      data: data,
      token: true
    }),
    payload: data
  };
}

export function remove(data) {
  return {
    types: [productCarCateTypes.DELETE_PRODUCTCARCATE, productCarCateTypes.DELETE_PRODUCTCARCATE_SUCCESS, productCarCateTypes.DELETE_PRODUCTCARCATE_FAIL],
    promise: (client) => client.del('/productCarCate', {
      data: data,
      token: true
    }),
    payload: data
  };
}


