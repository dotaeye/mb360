import React ,{PropTypes}from 'react';
import {reduxForm} from 'redux-form';
import {createValidator, required, minLength, email} from '../../utils/validation';
const fields = ['username', 'password'];

const validate = createValidator({
  username: {
    rules: [required, email],
    displayName: '用户名'
  },
  password: {
    rules: [required, minLength(8)],
    displayName: '密码'
  }
});

var LoginForm = React.createClass({

  propTypes: {
    fields: PropTypes.object.isRequired,
    handleSubmit: PropTypes.func.isRequired,
    submitting: PropTypes.bool.isRequired
  },

  render(){

    const {fields: {username, password}, handleSubmit, submitting} = this.props;
    const userError = username.touched && username.error;
    const passwordError = password.touched && password.error;
    return (
      <form onSubmit={handleSubmit}>
        <div className='form-validate top'>
          {userError && <div className='form-validate-item'>{username.error}</div>}
          {passwordError && <div className='form-validate-item'>{password.error}</div>}
        </div>
        <div className='form-input-area'>
          <div className={`form-group ${(userError?'error':'')}`}>
            <input type="text" className='form-control' placeholder="Username" {...username}/>
          </div>
          <div className={`form-group ${(passwordError?'error':'')}`}>
            <input type="password" className='form-control' placeholder="Password" {...password}/>
          </div>
        </div>
        <div className='form-group'>
          <button disabled={submitting} onClick={handleSubmit} type='submit' className='btn btn-default'>
            {submitting ? <i/> : <i/>} Submit
          </button>
        </div>
      </form>
    );
  }
});

export default reduxForm({
  form: 'loginForm',
  fields,
  validate
})(LoginForm);

