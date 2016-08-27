import React ,{PropTypes}from 'react';
import {reduxForm} from 'redux-form';
import ApiClient from '../../utils/ApiClient';
import {createValidator, required, maxLength, email,equalTo} from '../../utils/validation';

const client = new ApiClient();
const fields = ['email', 'password', 'confirmPassword'];

const validate = createValidator({
    email: [required, email],
    password: [required, maxLength(10)],
    confirmPassword: [required, maxLength(10), equalTo('password','The password and confirmation password do not match.')]
});

function asyncValidate(data) {
    // TODO: figure out a way to move this to the server. need an instance of ApiClient
    if (!data.email) {
        return Promise.resolve({});
    }
    return new Promise((resolve, reject) => {
        client.get('/account/validateEmail', {
            params: {
                email: data.email
            }
        }).then((res)=> {
            const errors = {};
            let valid = res;
            if (valid) {
                resolve();
            } else {
                errors.email = 'email has already exist!';
                reject(errors);
            }
        })
    });
}

var RegisterForm = React.createClass({

    propTypes: {
        fields: PropTypes.object.isRequired,
        handleSubmit: PropTypes.func.isRequired,
        submitting: PropTypes.bool.isRequired,
        formError: PropTypes.array
    },

    render(){

        const {fields: {email, password,confirmPassword}, handleSubmit, submitting} = this.props;

        return (
            <form onSubmit={handleSubmit}>
                <div className='form-group'>

                    <label>Email</label>

                    <input type="text" className='form-control' placeholder="Email" {...email}/>

                    {email.touched && email.error && <div>{email.error}</div>}
                </div>

                <div className='form-group'>
                    <label>Password</label>

                    <input type="password" className='form-control' placeholder="Password" {...password}/>

                    {password.touched && password.error && <div>{password.error}</div>}
                </div>

                <div className='form-group'>
                    <label>confirmPassword</label>

                    <input type="password" className='form-control' placeholder="confirmPassword" {...confirmPassword}/>

                    {confirmPassword.touched && confirmPassword.error && <div>{confirmPassword.error}</div>}
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
    form: 'registerForm',
    fields,
    validate,
    asyncValidate,
    asyncBlurFields: ['email'],
})(RegisterForm);

