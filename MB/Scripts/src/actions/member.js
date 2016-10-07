import * as memberTypes from '../contants/member';

export function getAll(params) {
  return {
    types: [memberTypes.GET_ALL_MEMBER, memberTypes.GET_ALL_MEMBER_SUCCESS, memberTypes.GET_ALL_MEMBER_FAIL],
    promise: (client) => client.get('/member',{
      token: true,
      params
    })
  };
}

export function getById(id) {
  return {
    types: [memberTypes.GET_ONE_MEMBER, memberTypes.GET_ONE_MEMBER_SUCCESS, memberTypes.GET_ONE_MEMBER_FAIL],
    promise: (client) => client.get('/member/'+id,{
      token: true
    })
  };
}

export function create(data) {
  return {
    types: [memberTypes.CREATE_MEMBER, memberTypes.CREATE_MEMBER_SUCCESS, memberTypes.CREATE_MEMBER_FAIL],
    promise: (client) => client.post('/member', {
      data: data,
      token: true
    })
  };
}

export function update(data) {
  return {
    types: [memberTypes.UPDATE_MEMBER, memberTypes.UPDATE_MEMBER_SUCCESS, memberTypes.UPDATE_MEMBER_FAIL],
    promise: (client) => client.put('/member', {
      data: data,
      token: true
    })
  };
}

export function remove(id) {
  return {
    types: [memberTypes.DELETE_MEMBER, memberTypes.DELETE_MEMBER_SUCCESS, memberTypes.DELETE_MEMBER_FAIL],
    promise: (client) => client.del('/member/'+id, {
      token: true
    })
  };
}


