import { combineReducers } from 'redux';
import { reducer as form } from 'redux-form';
import auth from './auth';
import userPermission from './userPermission';
import userRole from './userRole';
import department from './department';

export default combineReducers({
    form,
    auth,
    userPermission,
    userRole,
    department
});
