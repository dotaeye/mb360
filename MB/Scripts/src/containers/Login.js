import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { browserHistory } from 'react-router';
import { connect } from 'react-redux'
import { Spin } from 'antd';
import * as actionCreators from '../actions/auth'
import { LoginForm } from '../components';
import connectStatic from '../utils/connectStatic'
import { message } from 'antd';


const Login = React.createClass({

  onSubmit(data){
    data.grant_type = 'password';
    this.props.actions.login(data);
  },

  componentWillReceiveProps(nextProps){
    if(!this.props.auth.token && nextProps.auth.token){
      browserHistory.push('/');
    }
    if(nextProps.auth.loginError){
      message.error(nextProps.auth.loginError.error_description);
      this.props.actions.clearLoginError();
    }
  },

  render() {
    let {auth:{loggingIn}} =this.props;
    return (
      <div id="login">
        <div className='login-form'>
          <h1>
           <a href="/" title="logo" >logo</a>
          </h1>
          <LoginForm onSubmit={this.onSubmit} submitting={loggingIn} ref='loginForm'/>
        </div>
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

const statics = {
    noLayout: true
};

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(Login))

