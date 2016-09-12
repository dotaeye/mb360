import * as authTypes from '../contants/auth';

const initialState = {
  loaded: false,
  permission: []
};

export default function auth(state = initialState, action = {}) {
  switch (action.type) {
    case authTypes.LOAD_PROFILE:
      return {
        ...state,
        loading: true
      };
    case authTypes.LOAD_PROFILE_SUCCESS:
      return {
        ...state,
        loading: false,
        profile: action.result
      };
    case authTypes.LOAD_PROFILE_FAIL:
      return {
        ...state,
        loading: false,
        error: action.error
      };
    case authTypes.SAVE_PROFILE:
      return {
        ...state,
        loading: true
      };
    case authTypes.SAVE_PROFILE_SUCCESS:
      return {
        ...state,
        loading: false
      };
    case authTypes.SAVE_PROFILE_FAIL:
      return {
        ...state,
        loading: false,
        saveProfileError: action.error
      };
    case authTypes.LOAD_PERMISSION_SUCCESS:
      return {
        ...state,
        permission: action.result
      };
    case authTypes.LOAD_AUTH_TOKEN:
      let token = action.result;
      if (token) {
        if (new Date(token['.expires']).getTime() < new Date().getTime()) {
          token = null;
        }
      }
      return {
        ...state,
        loaded: true,
        token: token
      };
    case authTypes.CLEAN_AUTH_TOKEN:
      return {
        ...state,
        token: null
      };
    case authTypes.REGISTER:
      return {
        ...state,
        registering: true
      };
    case authTypes.REGISTER_SUCCESS:
      return {
        ...state,
        registering: false
      };
    case authTypes.REGISTER_FAIL:
      return {
        ...state,
        registering: false,
        registerError: action.error
      };
    case authTypes.LOGIN:
      return {
        ...state,
        loggingIn: true
      };
    case authTypes.LOGIN_SUCCESS:
      return {
        ...state,
        loggingIn: false,
        token: action.result
      };
    case authTypes.LOGIN_FAIL:
      return {
        ...state,
        loggingIn: false,
        token: null,
        loginError: action.error
      };
    case authTypes.CLEAR_LOGIN_ERROR:
      return {
        ...state,
        loginError: null
      };
    case authTypes.LOGOUT:
      return {
        ...state,
        loggingOut: true
      };
    case authTypes.LOGOUT_SUCCESS:
      return {
        ...state,
        loggingOut: false,
        token: null
      };
    case authTypes.LOGOUT_FAIL:
      return {
        ...state,
        loggingOut: false,
        logoutError: action.error
      };
    default:
      return state;
  }
}

