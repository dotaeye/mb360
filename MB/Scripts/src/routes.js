import React from 'react';
import _ from 'lodash';
import {IndexRoute, Route} from 'react-router';
import { loadAuthToken, loadPermission, cleanAuthToken } from './actions/auth';
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
  CityCate,
  Job,
  Storage,
  Member,
  MemberCreateOrUpdate,
  Product,
  ProductCreateOrUpdate,
  ProductStorageQuantity,
  ProductCarCate,
  ProductAttribute,
  ProductAttributeMapping,
  ProductSpecificationAttribute,
  SpecificationAttribute,
  SpecificationAttributeOption,
  Manufacturer,
  NotFound
} from './containers';

export default (store) => {
  const requireLogin = (controller, action ,nextState, replace, cb) => {
    function checkAuth() {
      const { auth: { token, permission }} = store.getState();
      let hasPermission = permission.find(x=>x.controller==controller&&x.action==action);
      if (!token || !hasPermission ) {
        store.dispatch(cleanAuthToken());
        baseStorage.getStorage(configs.storage).remove(configs.authToken);
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
          
      <Route path='citycate' component={CityCate} onEnter={requireLogin.bind(null,'citycate','index')} />

      <Route path='storage' component={Storage} onEnter={requireLogin.bind(null,'storage','index')} />

      <Route path='member' component={Member} onEnter={requireLogin.bind(null,'member','index')} />

      <Route path='member/create' component={MemberCreateOrUpdate} onEnter={requireLogin.bind(null,'member','create')} />

      <Route path='member/update/:id' component={MemberCreateOrUpdate} onEnter={requireLogin.bind(null,'member','update')} />

      <Route path='product' component={Product} onEnter={requireLogin.bind(null,'product','index')} />

      <Route path='product/create' component={ProductCreateOrUpdate} onEnter={requireLogin.bind(null,'product','create')} />

      <Route path='product/update/:id' component={ProductCreateOrUpdate} onEnter={requireLogin.bind(null,'product','update')} />
      
      <Route path='productstoragequantity/:id' component={ProductStorageQuantity} onEnter={requireLogin.bind(null,'productstoragequantity','index')} />

      <Route path='productcarcate/:id' component={ProductCarCate} onEnter={requireLogin.bind(null,'productcarcate','index')} />

      <Route path='productattribute' component={ProductAttribute} onEnter={requireLogin.bind(null,'productattribute','index')} />
      
      <Route path='productattributemapping/:id' component={ProductAttributeMapping} onEnter={requireLogin.bind(null,'productattributemapping','index')} />

      <Route path='productspecificationattribute/:id' component={ProductSpecificationAttribute} onEnter={requireLogin.bind(null,'productspecificationattribute','index')} />

      <Route path='manufacturer' component={Manufacturer} onEnter={requireLogin.bind(null,'manufacturer','index')} />

      <Route path='specificationattribute' component={SpecificationAttribute} onEnter={requireLogin.bind(null,'specificationattribute','index')} />

      <Route path='specificationattributeoption/:id' component={SpecificationAttributeOption} onEnter={requireLogin.bind(null,'specificationattributeoption','index')} />

      <Route path='login' component={Login}/>

      <Route path='register' component={Register}/>

      <Route path='profile' component={Profile} onEnter={requireLogin.bind(null,'userpermission','index')}/>

      { /* Catch all route */ }
      <Route path='*' component={NotFound} status={404}/>
    </Route>
  );
};
