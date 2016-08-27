import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux'
import { Spin, Table, Icon, Button, Modal, Form, Input, Checkbox, message } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as authActions from '../actions/auth'
import * as userRoleActions from '../actions/userRole'
import * as userPermissionActions from '../actions/userPermission'
import { ProfileForm } from '../components';
import GroupCheckList from '../components/control/GroupCheckList';
import { getGroupSelectData } from '../utils/biz';

import _ from 'lodash';
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;

var UserRole = React.createClass({

  displayName: 'UserRole',

  getInitialState() {
    return {
      pagination: {
        pageSize: 10,
        current: 1
      },
      record: {},
      loadPermission: false
    };
  },

  fetchData(page){
    this.props.userRoleActions.getAll({
      results: this.state.pagination.pageSize,
      page: page || this.state.pagination.current
    });
    if (!this.state.loadPermission) {
      this.props.userPermissionActions.getAll({
        results: 1000,
        page: 1
      }).then(()=> {
        this.setState({
          loadPermission: true
        })
      });
    }
  },

  componentDidMount(){
    this.fetchData();
  },

  handleTableChange(pagination, filters, sorter) {
    const pager = this.state.pagination;
    pager.current = pagination.current;
    this.setState({
      pagination: pager,
    });
    this.props.userRoleActions.getAll({
      results: pagination.pageSize,
      page: pagination.current,
      sortField: sorter.field,
      sortOrder: sorter.order,
    });
  },
  onAdd(){
    this.setState({
      visible: true,
      edit: false,
      title: '添加角色',
      record: {}
    });
  },

  onEdit(record){
    this.setState({
      visible: true,
      edit: true,
      title: '编辑角色',
      record
    });
  },

  onRemove(record){
    const self = this;
    const { userRole:{ list }} = this.props;
    let source = list.data;
    const remove = this.props.userRoleActions.remove;
    confirm({
      title: '确认删除该权限？',
      onOk() {
        remove(record.id).then((err)=> {
          if (err) {
            message.error('删除操作执行失败！请刷新页面尝试。');
          } else {
            message.success('删除操作执行成功！');
            let data = _.remove(source, (item)=> {
              return item.id === record.id
            });
            let page = self.state.pagination.current;
            if (data.length === 0) {
              page = page - 1;
            }
            self.fetchData(_.max([page - 1, 1]));
          }
        });
      },
      onCancel() {
      }
    });
  },

  onModalSubmit(){
    const { edit, record }=this.state;
    const { update, create} =this.props.userRoleActions;
    this.props.form.validateFields((errors, formdata) => {
      if (!!errors) {
        console.log('Errors in form!!!');
        return;
      }
      if (edit) {
        formdata.id = record.id;
        update(formdata).then((err)=> {
          if (err) {
            message.error('更新数据失败。');
          } else {
            message.success('更新数据成功！');
            this.props.form.resetFields();
            this.fetchData();
          }
        });
      } else {
        create(formdata).then((err)=> {
          if (err) {
            message.error('创建数据失败。');
          } else {
            message.success('创建数据成功！');
            this.props.form.resetFields();
            this.fetchData();
          }
        });
      }
      this.onModalClose();
    });
  },

  onModalClose(){
    this.setState({visible: false}, ()=> {
      this.props.form.resetFields();
    });
  },

  render() {
    const columns = [{
      title: 'Id',
      dataIndex: 'id',
      sorter: true,
      width: '20%',
    }, {
      title: '名称',
      dataIndex: 'name',
    }, {
      title: '操作',
      key: 'operation',
      render: (text, record) => (
        <span>
          <Button type="ghost" shape="circle" icon="edit" size="small" title='编辑'
                  onClick={this.onEdit.bind(null,record)}/>
          <span className="ant-divider"></span>
          <Button type="ghost" shape="circle" icon="delete" size="small" title='删除'
                  onClick={this.onRemove.bind(null,record)}/>
        </span>
      )
    }];

    const { userRole:{ loading, error, list }, userPermission} = this.props;
    const groupPermissions = getGroupSelectData(userPermission.list ? userPermission.list.data : [], 'group');
    const { record, title, visible  }=this.state;
    const data = list ? list.data : [];
    const pagination = Object.assign({}, this.state.pagination, {total: list ? list.pageCount : 0})
    const { getFieldProps } = this.props.form;
    const formItemLayout = {
      labelCol: {span: 4},
      wrapperCol: {span: 20}
    };

    return (
      <div className='container'>
        <div className='ant-list-header' data-flex="dir:right">
          <div className='ant-list-header-right'>
            <Button type="primary" icon="plus" onClick={this.onAdd}>添加角色</Button>
          </div>
        </div>
        <Table
          ref='table'
          columns={columns}
          rowKey={record => record.id}
          dataSource={data}
          pagination={pagination}
          loading={loading}
          onChange={this.handleTableChange}
          />
        <Modal title={title} visible={visible} onOk={this.onModalSubmit}
               onCancel={this.onModalClose}>
          <Form horizontal>
            <FormItem
              {...formItemLayout}
              label="名称"
              >
              <Input  {...getFieldProps('name', {
                initialValue: record.name,
                rules: [{required: true, message: '请输入名称'}]
              })} type="text"/>
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="权限"
              >
              <GroupCheckList  {...getFieldProps('permission', {
                initialValue: record.permission,
              })} groups={groupPermissions}/>
            </FormItem>
          </Form>
        </Modal>
      </div>
    );
  }
});

function mapStateToProps(state) {
  return {
    auth: state.auth,
    userRole: state.userRole,
    userPermission: state.userPermission
  }
}

function mapDispatchToProps(dispatch) {
  return {
    authActions: bindActionCreators(authActions, dispatch),
    userRoleActions: bindActionCreators(userRoleActions, dispatch),
    userPermissionActions: bindActionCreators(userPermissionActions, dispatch)
  }
}

const statics = {
  path: 'userrole',
  menuGroup:'system',
  breadcrumb: [{
    title: '系统设置'
  }, {
    title: '角色管理'
  }]
};

UserRole = createForm()(UserRole);

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(UserRole))

