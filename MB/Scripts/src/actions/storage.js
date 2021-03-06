// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


import * as storageTypes from '../contants/storage';

export function getAll(params) {
  return {
    types: [storageTypes.GET_ALL_STORAGE, storageTypes.GET_ALL_STORAGE_SUCCESS, storageTypes.GET_ALL_STORAGE_FAIL],
    promise: (client) => client.get('/storage',{
      token: true,
      params
    })
  };
}

export function getById(id) {
  return {
    types: [storageTypes.GET_ONE_STORAGE, storageTypes.GET_ONE_STORAGE_SUCCESS, storageTypes.GET_ONE_STORAGE_FAIL],
    promise: (client) => client.get('/storage/'+id,{
      token: true
    })
  };
}

export function getStorageSelectList() {
  return {
    types: [storageTypes.GET_STORAGE_SELECTLIST, storageTypes.GET_STORAGE_SELECTLIST_SUCCESS, storageTypes.GET_STORAGE_SELECTLIST_FAIL],
    promise: (client) => client.get('/storage/selectlist',{
      token: true
    })
  };
}

export function create(data) {
  return {
    types: [storageTypes.CREATE_STORAGE, storageTypes.CREATE_STORAGE_SUCCESS, storageTypes.CREATE_STORAGE_FAIL],
    promise: (client) => client.post('/storage', {
      data: data,
	    token: true
    })
  };
}

export function update(data) {
  return {
    types: [storageTypes.UPDATE_STORAGE, storageTypes.UPDATE_STORAGE_SUCCESS, storageTypes.UPDATE_STORAGE_FAIL],
    promise: (client) => client.put('/storage', {
      data: data,
	    token: true
    })
  };
}

export function remove(id) {
  return {
    types: [storageTypes.DELETE_STORAGE, storageTypes.DELETE_STORAGE_SUCCESS, storageTypes.DELETE_STORAGE_FAIL],
    promise: (client) => client.del('/storage/'+id, {
		token: true
    })
  };
}


