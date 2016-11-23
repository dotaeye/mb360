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
import member from './member'
import product from './product'
import productStorageQuantity from './productStorageQuantity'
import productCarCate from './productCarCate' 
import productAttribute from './productAttribute'
import productAttributeValue from './productAttributeValue'
import productAttributeMapping from './productAttributeMapping'
import specificationAttribute from './specificationAttribute'
import specificationAttributeOption from './specificationAttributeOption'
import productSpecificationAttribute from './productSpecificationAttribute'
import manufacturer from './manufacturer'
import job from './job';
import banner from './banner';

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
    member,
    product,
    productStorageQuantity,
    productCarCate,
    productAttribute,
    productAttributeValue,
    productAttributeMapping,
    productSpecificationAttribute,
    specificationAttribute,
    specificationAttributeOption,
    manufacturer,
    banner
});
