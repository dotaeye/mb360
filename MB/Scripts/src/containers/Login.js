import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { browserHistory } from 'react-router';
import { connect } from 'react-redux'
import * as actionCreators from '../actions/auth'
import { LoginForm } from '../components';
import connectStatic from '../utils/connectStatic'
import { Spin, Table, Icon, Button, Modal, Form, Input, Checkbox, message,Select } from 'antd';
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;


var Login = React.createClass({

  onSubmit(){
    this.props.form.validateFields((errors, formdata) => {
      if (!!errors) {
        console.log('Errors in form!!!');
        return;
      }
      formdata.grant_type = 'password';
      formdata.type = 'password';
      this.props.actions.login(formdata,()=>{
        browserHistory.push('/admin');
      },()=>{
        const {auth}=this.props;
        message.error(auth.loginError.error_description);
        this.props.actions.clearLoginError();
      });
    });
  },

  render() {
    let {auth:{loggingIn}} =this.props;
    const { getFieldDecorator } = this.props.form;
    const formItemLayout = {
      labelCol: {span: 4},
      wrapperCol: {span: 20}
    };
    return (
      <div id="login">
        <div className='login-form'>
          <h1>
            <a href="/" title="logo">logo</a>
          </h1>
          <Form horizontal>
            <FormItem
              {...formItemLayout}
              label="用户名"
              >
              {getFieldDecorator('username', {
                  rules: [{required: true, message: '请输入用户名'},{type: 'email', message: '请输入正确的邮箱地址'}]
                }
              )(
                <Input  type="text"/>
              )}
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="密码"
              >
              {getFieldDecorator('password', {
                  rules: [{required: true, message: '请输入密码'}]
                }
              )(
                <Input type="password"/>
              )}
            </FormItem>
            <FormItem wrapperCol={{ span: 7, offset: 4 }}>
              <Button type="primary" onClick={this.onSubmit} disabled={loggingIn}>{loggingIn?'登录中...':'登陆'}</Button>
            </FormItem>
          </Form>
        </div>
      </div>
    );
  }
});

function mapStateToProps(state) {
  return {
    auth: state.auth
  }
}

function mapDispatchToProps(dispatch) {
  return {actions: bindActionCreators(actionCreators, dispatch)}
}

const statics = {
  noLayout: true
};

Login = createForm()(Login);

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(Login))

