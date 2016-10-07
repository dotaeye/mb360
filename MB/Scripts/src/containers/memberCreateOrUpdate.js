import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { Link, browserHistory } from 'react-router';
import { connect } from 'react-redux'
import { Alert, Spin, Table, Icon, Button, Modal, Form, Input, InputNumber ,Checkbox, Tabs, message,Select, Cascader, TreeSelect } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as authActions from '../actions/auth'
import * as memberActions from '../actions/member'
import * as jobActions from '../actions/job'
import * as userRoleActions from '../actions/userRole'
import _ from 'lodash';
import { setCascadeValues } from '../utils/biz';
import UploadFile from '../components/control/UploadFile'
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;
const Option = Select.Option;
const TabPane = Tabs.TabPane;

var MemberCreateOrUpdate = React.createClass({

  displayName: 'MemberCreateOrUpdate',

  fetchData(){
    const { routeParams:{id} } =this.props;
    const promise = [];
    promise.push(this.props.jobActions.getCascader());
    promise.push(this.props.userRoleActions.getSelectList());
    Promise.all(promise).then(err=> {
      if (id) {
        this.props.memberActions.getById(id).then(err=> {
          this.setState({
            loading: false
          })
        })
      } else {
        this.setState({
          loading: false
        })
      }
    });
  },

  getInitialState(){
    return {
      addSuccessed: false,
      loading: true
    }
  },

  componentWillUnMount(){
    console.log('componentWillUnMount')
  },

  componentDidMount(){
    this.fetchData();
  },

  onModalSubmit(){
    const { routeParams:{id} }=this.props;
    const { update, create} =this.props.memberActions;
    const { entity }= this.props.member;
    this.props.form.validateFields((errors, formdata) => {
      if (!!errors) {
        console.log('Errors in form!!!');
        return;
      }
      this.setState({
        loading: true
      })
      formdata.jobId = formdata.jobId[formdata.jobId.length - 1];
      if (id) {
        formdata.id = entity.id;
        update(formdata).then((err)=> {
          if (err) {
            message.error('更新数据失败。');
          } else {
            message.success('更新数据成功！');
            this.setState({
              loading: false
            });
          }
        });
      } else {

        create(formdata).then((err)=> {
          if (err) {
            message.error('创建数据失败。');
          } else {
            message.success('创建数据成功！');
            this.setState({
              loading: false,
              hasAdd: true
            }, ()=> {
              const newEntity = this.props.member.entity;
              if (newEntity.id) {
                browserHistory.push('/admin/member/update/' + newEntity.id);
              }
            })
          }
        });
      }
    });
  },

  render() {
    const { member:{ entity }, routeParams:{id}} = this.props;
    const { loading , hasAdd }=this.state;
    const { cascader }=this.props.job;
    const { getFieldDecorator } = this.props.form;
    const record = (id || hasAdd) ? entity : {};
    const { selectlist }= this.props.userRole;
    let jobValues = [];
    if (record.jobId) {
      setCascadeValues(cascader, record.jobId, jobValues);
      jobValues = jobValues.reverse();
    }
    const formItemLayout = {
      labelCol: {span: 4},
      wrapperCol: {span: 20}
    };
    return (
      <div className='container'>
        <div className='ant-list-header' data-flex="main:justify">
          <div className='ant-list-header-left'>
            <Link to='member'>
              <Icon type='arrow-left'/> 返回列表
            </Link>
          </div>
          <div className='ant-list-header-right'>
            <Button type="primary" icon="save" onClick={this.onModalSubmit}>保存</Button>
          </div>
        </div>
        {id && (
          <div className='nav-tabs-container'>
            <ul className="nav nav-tabs">
              <li className="active"><a>基本信息</a></li>

            </ul>
          </div>
        )}
        <Spin spinning={loading}>
          <Form horizontal>
            <FormItem
              {...formItemLayout}
              label="邮箱地址"
              >
              {getFieldDecorator('userName', {
                  initialValue: record.userName,
                  rules: [{
                    required: true,
                    message: '请输入邮箱地址'
                  }, {
                    type: 'email',
                    message: '请输入正确的邮箱地址'
                  },
                  ]
                }
              )(
                <Input type="text"/>
              )}
            </FormItem>
            {!id && (
              <FormItem
                {...formItemLayout}
                label="密码"
                >
                {getFieldDecorator('password', {
                    initialValue: record.password,
                    rules: [{required: true, message: '请输入密码'}]
                  }
                )(
                  <Input type="password"/>
                )}
              </FormItem>
            )}
            <FormItem
              {...formItemLayout}
              label="手机"
              >
              {getFieldDecorator('phoneNumber', {
                  initialValue: record.phoneNumber,
                  rules: [{required: true, message: '请输入手机号码'}]
                }
              )(
                <Input type="text"/>
              )}
            </FormItem>

            <FormItem
              {...formItemLayout}
              label="职位"
              >
              {getFieldDecorator('jobId', {
                  initialValue: jobValues,
                  rules: [{required: true, type: 'array', message: '请选择职位'}]
                }
              )(
                <Cascader placeholder='请选择职位' options={cascader}/>
              )}
            </FormItem>

            <FormItem
              {...formItemLayout}
              label="角色"
              >
              {getFieldDecorator('userRoleId', {
                initialValue: record.userRoleId ? `${record.userRoleId}` : '',
                rules: [{required: true, message: '请选择角色'}]
              })(
                <Select placeholder='请选择角色'>
                  {selectlist.map(item=> {
                    return (
                      <Option key={item.id}>{item.name}</Option>
                    )
                  })}
                </Select>
              )}
            </FormItem>


            <FormItem
              {...formItemLayout}
              label="电话确认"
              >
              {getFieldDecorator('phoneNumberConfirmed', {
                initialValue: record.phoneNumberConfirmed,
                valuePropName: 'checked'
              })(
                <Checkbox/>
              )}
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="邮箱确认"
              >
              {getFieldDecorator('emailConfirmed', {
                initialValue: record.emailConfirmed,
                valuePropName: 'checked'
              })(
                <Checkbox/>
              )}
            </FormItem>
          </Form>
        </Spin>
      </div>
    );
  }
});

function mapStateToProps(state) {
  return {
    auth: state.auth,
    member: state.member,
    job: state.job,
    userRole: state.userRole
  }
}

function mapDispatchToProps(dispatch) {
  return {
    authActions: bindActionCreators(authActions, dispatch),
    memberActions: bindActionCreators(memberActions, dispatch),
    jobActions: bindActionCreators(jobActions, dispatch),
    userRoleActions: bindActionCreators(userRoleActions, dispatch)
  }
}

const statics = {
  path: 'member/create',
  menuGroup: 'member',
  breadcrumb: [{
    title: '产品中心'
  }, {
    title: '产品信息'
  }]
};

MemberCreateOrUpdate = createForm()(MemberCreateOrUpdate);

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(MemberCreateOrUpdate))
