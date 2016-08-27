import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux'
import { Route, Link} from 'react-router';
import { Spin } from 'antd';
import * as actionCreators from '../actions/auth'
import { RegisterForm } from '../components';

import connectStatic from '../utils/connectStatic'


const Register = React.createClass({

    onSubmit(data){
        this.props.actions.register(data);
    },

    render() {

        let {auth:{registering, registerError}} =this.props;

        return (
            <div id="register" className='container'>
                <h1>Register Page</h1>
                <RegisterForm onSubmit={this.onSubmit} submitting={registering} formError={registerError}
                              ref='registerForm'/>
            </div>
        );
    }
});

function mapStateToProps(state) {
    return {
        auth: state.auth,
        form: state.form
    }
}

function mapDispatchToProps(dispatch) {
    return {actions: bindActionCreators(actionCreators, dispatch)}
}

export default connect(mapStateToProps, mapDispatchToProps)(Register)

