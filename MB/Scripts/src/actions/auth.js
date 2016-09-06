import * as authTypes from '../contants/auth';
import configs from '../configs'

export function loadProfile() {
  return {
    types: [authTypes.LOAD_PROFILE, authTypes.LOAD_PROFILE_SUCCESS, authTypes.LOAD_PROFILE_FAIL],
    promise: (client) => client.get('/account/profile', {
      token: true,
    })
  };
}
export function saveProfile(data) {
  return {
    types: [authTypes.SAVE_PROFILE, authTypes.SAVE_PROFILE_SUCCESS, authTypes.SAVE_PROFILE_FAIL],
    promise: (client) => client.post('/account/profile', {
      token: true,
      data: data
    })
  };
}

export function register(data) {
  return {
    types: [authTypes.REGISTER, authTypes.REGISTER_SUCCESS, authTypes.REGISTER_FAIL],
    promise: (client) => client.post('/account/register', {
      data: data
    })
  };
}


export function loadPermission() {
  return {
    types: [authTypes.LOAD_PERMISSION, authTypes.LOAD_PERMISSION_SUCCESS, authTypes.LOAD_PERMISSION_FAIL],
    promise: (client) => client.get('/account/loadpermission', {
      token: true
    })
  };
}

export function loadAuthToken(token) {
  return {
    type: authTypes.LOAD_AUTH_TOKEN,
    result: token
  }
}

export function clearLoginError(token) {
  return {
    type: authTypes.CLEAR_LOGIN_ERROR
  }
}

export function login(data) {
  return {
    types: [authTypes.LOGIN, authTypes.LOGIN_SUCCESS, authTypes.LOGIN_FAIL],
    promise: (client) => client.post('/account/login', {
      data: data,
      formEncoding: true,
      save: {
        key: configs.authToken,
        expired: 60 * 24 * 14
      }
    })
  };
}


export function logout() {
  return {
    types: [authTypes.LOGOUT, authTypes.LOGOUT_SUCCESS, authTypes.LOGOUT_FAIL],
    promise: (client) => client.post('/account/logout', {
      token: true,
      clear: {
        key: configs.authToken
      }
    })
  };
}