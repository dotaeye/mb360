import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux'
import { Spin, Table, Icon, Button, Modal, Form, Input, Checkbox, message,Select } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as authActions from '../actions/auth'
import * as userPermissionActions from '../actions/userPermission'
import { ProfileForm } from '../components';
import _ from 'lodash';
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;
const Option = Select.Option;

var UserPermission = React.createClass({

  displayName: 'UserPermission',

  getInitialState() {
    return {
      pagination: {
        pageSize: 10,
        current: 1
      }
    };
  },

  fetchData(page){
    this.props.userPermissionActions.getAll({
      results: this.state.pagination.pageSize,
      page: page || this.state.pagination.current
    });
  },

  componentDidMount(){
    this.fetchData();
  },

  handleTableChange(pagination, filters, sorter) {
    const pager = this.state.pagination;
    pager.current = pagination.current;
    this.setState({
      pagination: pager
    });
    this.props.userPermissionActions.getAll({
      results: pagination.pageSize,
      page: pagination.current,
      sortField: sorter.field,
      sortOrder: sorter.order
    });
  },
  onAdd(){
    this.setState({
      visible: true,
      edit: false,
      title: '添加权限',
      record: {}
    });
  },

  onEdit(record){
    this.props.userPermissionActions.getById(record.id).then((err)=> {
      if (err) {
        message.error('获取权限数据失败！请刷新页面尝试。');
      }
      else {
        this.setState({
          visible: true,
          edit: true,
          title: '编辑权限'
        });
      }
    });
  },

  onRemove(record){
    const self = this;
    const { userPermission:{ list }} = this.props;
    let source = list.data;
    const remove = this.props.userPermissionActions.remove;
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
    const { edit }=this.state;
    const { update, create} =this.props.userPermissionActions;
    const { entity }= this.props.userPermission;
    this.props.form.validateFields((errors, formdata) => {
      if (!!errors) {
        console.log('Errors in form!!!');
        return;
      }
      if (edit) {
        formdata.id = entity.id;
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
      width: '20%'
    }, {
      title: '名称',
      dataIndex: 'name'
    }, {
      title: '控制器',
      dataIndex: 'controller'
    }, {
      title: 'Action',
      dataIndex: 'action'
    }, {
      title: '分组',
      dataIndex: 'group'
    }, {
      title: '是否为API',
      dataIndex: 'isApi',
      render: isApi=>isApi ? '是' : '否'
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
    const { userPermission:{ loading, list, entity }} = this.props;
    const { title, visible, edit }=this.state;
    const data = list ? list.data : [];
    const pagination = Object.assign({}, this.state.pagination, {total: list ? list.recordCount : 0})
    const { getFieldDecorator } = this.props.form;
    const record = edit ? entity : {};
    const formItemLayout = {
      labelCol: {span: 4},
      wrapperCol: {span: 20}
    };
    const permissionGroups = [
      '后台权限',
      '系统设置',
      '业务中心',
      '产品中心',
      '用户中心'
    ];
    return (
      <div className='container'>
        <div className='ant-list-header' data-flex="dir:right">
          <div className='ant-list-header-right'>
            <Button type="primary" icon="plus" onClick={this.onAdd}>添加权限</Button>
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
              {getFieldDecorator('name', {
                  initialValue: record.name,
                  rules: [{required: true, message: '请输入名称'}]
                }
              )(
                <Input type="text"/>
              )}
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="控制器"
              >
              {getFieldDecorator('controller', {
                initialValue: record.controller,
                rules: [{required: true, message: '请输入控制器'}]
              })(
                <Input type="text"/>
              )}

            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Action"
              >
              {getFieldDecorator('action', {
                initialValue: record.action,
                rules: [{required: true, message: '请输入Action'}]
              })(
                <Input type="text"/>
              )}
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="分组"
              >
              {getFieldDecorator('group', {
                initialValue: record.group,
                rules: [{required: true, message: '请选择权限分组'}]
              })(
                <Select>
                  {permissionGroups.map((g,gIndex)=>{
                    return (
                      <Option key={gIndex} value={g}>{g}</Option>
                    )
                  })}
                </Select>
              )}

            </FormItem>
            <FormItem
              {...formItemLayout}
              label="是否为API"
              >
              {getFieldDecorator('isApi', {initialValue: record.isApi, valuePropName: 'checked'})(
                <Checkbox />
              )}
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
    userPermission: state.userPermission
  }
}

function mapDispatchToProps(dispatch) {
  return {
    authActions: bindActionCreators(authActions, dispatch),
    userPermissionActions: bindActionCreators(userPermissionActions, dispatch)
  }
}

const statics = {
  path: 'userpermission',
  menuGroup: 'system',
  breadcrumb: [{
    title: '系统设置'
  }, {
    title: '权限管理'
  }]
};

UserPermission = createForm()(UserPermission);

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(UserPermission))

