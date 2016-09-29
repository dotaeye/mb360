import { combineReducers } from 'redux';
import { reducer as form } from 'redux-form';
import auth from './auth';
import userPermission from './userPermission';
import userRole from './userRole';
import department from './department';
import category from './category';
import carCate from './carCate';
import cityCate from './cityCate';
import storage from './storage';
import product from './product'
import productStorageQuantity from './productStorageQuantity'
import productCarCate from './productCarCate' 
import productAttribute from './productAttribute'
import productAttributeMapping from './productAttributeMapping'
import manufacturer from './manufacturer'
import job from './job';

export default combineReducers({
    form,
    auth,
    userPermission,
    userRole,
    department,
    category,
    job,
    carCate,
    cityCate,
    storage,
    product,
    productStorageQuantity,
    productCarCate,
    productAttribute,
    productAttributeMapping,
    manufacturer
});
