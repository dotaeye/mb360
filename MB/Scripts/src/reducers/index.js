import { combineReducers } from 'redux';
import { reducer as form } from 'redux-form';
import auth from './auth';
import userPermission from './userPermission';
import userRole from './userRole';
import department from './department';
import category from './category';
import carCate from './carCate';
import job from './job';

export default combineReducers({
    form,
    auth,
    userPermission,
    userRole,
    department,
    category,
    job,
    carCate
});
