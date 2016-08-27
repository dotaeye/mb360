import { combineReducers } from 'redux';
import { reducer as form } from 'redux-form';
import auth from './auth';
import userPermission from './userPermission';
import userRole from './userRole';

export default combineReducers({
    form,
    auth,
    userPermission,
    userRole
});
