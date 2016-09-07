import React from 'react';
import _ from 'lodash';
import {IndexRoute, Route} from 'react-router';
import { loadAuthToken,loadPermission } from './actions/auth';
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
  Category,
  CarCate,
  Job,
  NotFound
} from './containers';

export default (store) => {
  const requireLogin = (controller, action ,nextState, replace, cb) => {
    function checkAuth() {
      const { auth: { token, permission }} = store.getState();
      let hasPermission = permission.find(x=>x.controller==controller&&x.action==action);
      hasPermission=true;
      if (!token || !hasPermission ) {
        // oops, not logged in, so can't be here!
        replace('/login');
      }
      cb();
    }

    const { auth: { loaded }} = store.getState();
    if (!loaded) {
      const authToken = baseStorage.getStorage(configs.storage).get(configs.authToken);
      if(authToken){
        store.dispatch(loadPermission()).then(()=>{
          store.dispatch(loadAuthToken(authToken));
          checkAuth();
        })
      }else{
        checkAuth();
      }
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

      <IndexRoute component={Home} onEnter={requireLogin.bind(null,'home','index')} />

      <Route path='userpermission' component={UserPermission} onEnter={requireLogin.bind(null,'userpermission','index')} />

      <Route path='userrole' component={UserRole} onEnter={requireLogin.bind(null,'userrole','index')} />

      <Route path='department' component={Department} onEnter={requireLogin.bind(null,'department','index')} />

      <Route path='job' component={Job} onEnter={requireLogin.bind(null,'job','index')} />

      <Route path='category' component={Category} onEnter={requireLogin.bind(null,'category','index')} />

      <Route path='carcate' component={CarCate} onEnter={requireLogin.bind(null,'carcate','index')} />

      <Route path='login' component={Login}/>

      <Route path='register' component={Register}/>

      <Route path='profile' component={Profile} onEnter={requireLogin.bind(null,'userpermission','index')}/>

      { /* Catch all route */ }
      <Route path='*' component={NotFound} status={404}/>
    </Route>
  );
};
