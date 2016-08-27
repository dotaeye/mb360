import React from 'react';
import {IndexRoute, Route} from 'react-router';
import { loadAuthToken } from './actions/auth';
import configs from './configs';
import baseStorage from './utils/baseStorage';
import {
  App,
  Home,
  Login,
  Register,
  Profile,
  UserPermission,
  UserRole,
  Department,
  NotFound
} from './containers';

export default (store) => {
  const requireLogin = (nextState, replace, cb) => {
    function checkAuth() {
      const { auth: { token }} = store.getState();
      if (!token) {
        // oops, not logged in, so can't be here!
        replace('/login');
      }
      cb();
    }
    const { auth: { loaded }} = store.getState();
    if (!loaded) {
      const authToken = baseStorage.getStorage(configs.storage).get(configs.authToken);
      store.dispatch(loadAuthToken(authToken));
      checkAuth();
    } else {
      checkAuth();
    }
  };

  /**
   * Please keep routes in alphabetical order
   */
  return (
    <Route path='/' component={App}>

      { /* Routes requiring login */ }

      <IndexRoute component={Home} onEnter={requireLogin} />

      <Route path='userpermission' component={UserPermission} onEnter={requireLogin} />

      <Route path='userrole' component={UserRole} onEnter={requireLogin} />

      <Route path='department' component={Department} onEnter={requireLogin} />

      <Route path='login' component={Login}/>

      <Route path='register' component={Register}/>

      <Route path='profile' component={Profile} onEnter={requireLogin}/>

      { /* Catch all route */ }
      <Route path='*' component={NotFound} status={404}/>
    </Route>
  );
};
