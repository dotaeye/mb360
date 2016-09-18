﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router';
import { Spin, Table, Icon, Button, Modal, Form, Input, InputNumber, Checkbox, message,Select } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as authActions from '../actions/auth'
import * as storageActions from '../actions/storage'
import * as productActions from '../actions/product'
import * as productStorageQuantityActions from '../actions/productStorageQuantity'
import _ from 'lodash';
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;
const Option = Select.Option;

var ProductStorageQuantity = React.createClass({

  displayName: 'ProductStorageQuantity',

  getInitialState() {
    return {
      pagination: {
        pageSize: 1000,
        current: 1
      }
    };
  },

  fetchData(page){
    const {routeParams:{id}}=this.props;
    var promise=[];
    promise.push(this.props.storageActions.getStorageSelectList());
    promise.push(this.props.productActions.getById(id));
    Promise.all(promise).then(err=>{
      this.props.productStorageQuantityActions.getAll({
        id,
        results: this.state.pagination.pageSize,
        page: page || this.state.pagination.current
      });
    })
  },

  componentDidMount(){
    this.fetchData();
  },

  handleTableChange(pagination, filters, sorter) {
    const pager = this.state.pagination;
    const {routeParams:{id}}=this.props;
    pager.current = pagination.current;
    this.setState({
      pagination: pager
    });
    this.props.productStorageQuantityActions.getAll({
      id,
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
      title: '添加产品库存',
      record: {}
    });
  },

  onEdit(record){
    this.props.productStorageQuantityActions.getById(record.id).then((err)=> {
      if (err) {
        message.error('获取产品库存数据失败！请刷新页面尝试。');
      }
      else {
        this.setState({
          visible: true,
          edit: true,
          title: '编辑产品库存'
        });
      }
    });
  },

  onRemove(record){
    const self = this;
    const { productStorageQuantity:{ list }} = this.props;
    let source = list.data;
    const remove = this.props.productStorageQuantityActions.remove;
    confirm({
      title: '确认删除该产品库存？',
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
    const {routeParams:{id}}=this.props;
    const { update, create} =this.props.productStorageQuantityActions;
    const { entity }= this.props.productStorageQuantity;
    this.props.form.validateFields((errors, formdata) => {
      if (!!errors) {
        console.log('Errors in form!!!');
        return;
      }
      formdata.productId=id;
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
    const selectlist = this.props.storage.selectlist;
    const columns = [{
      title: 'Id',
      dataIndex: 'id',
      sorter: true,
      width: '20%'
    }, {
      title: '仓库',
      dataIndex: 'storageId',
      render: (storageId)=> selectlist.find(x=>x.id==storageId).name
    },{
      title: '数量',
      dataIndex: 'quantity'
    },{
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
    const { productStorageQuantity:{ loading, list, entity }} = this.props;
    const product = this.props.product.entity;

    const { title, visible, edit }=this.state;
    const data = list ? list.data : [];
    const record = edit ? entity : {};
    let selectdata=selectlist.filter(item=>data.filter(d=>d.storageId==item.id).length==0 ||record.storageId==item.id);
    const pagination = Object.assign({}, this.state.pagination, {total: list ? list.recordCount : 0})
    const { getFieldProps } = this.props.form;

    const formItemLayout = {
      labelCol: {span: 4},
      wrapperCol: {span: 20}
    };

    return (
      <div className='container'>
        <div className='ant-list-header' data-flex="main:justify">

          <Link to={`product/update/${product.id}`} >
              <Icon type='arrow-left'/> 返回 {product.name} 
          </Link>

          <div className='ant-list-header-right'>
            <Button type="primary" icon="plus" onClick={this.onAdd}>添加产品库存</Button>
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
              label="仓库"
              >
              <Select {...getFieldProps('storageId', {
                  initialValue: record.storageId?`${record.storageId}`:'',
                  rules: [{required: true, message: '请选择仓库'}]
                }
              )} placeholder='请选择仓库' >
                {selectdata.map(item=>{
                  return (
                    <Option key={item.id} >{item.name}</Option>
                  )
                })}
              </Select>
           </FormItem>

           <FormItem 
                 {...formItemLayout}
              label="数量" >
              <InputNumber step={1}  {...getFieldProps('quantity',{
                   initialValue: record.quantity,
                    rules: [{required: true, type:'number', message: '请输入库存数量'}]
              })} />
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
    storage: state.storage,
    product: state.product,
    productStorageQuantity: state.productStorageQuantity
  }
}

function mapDispatchToProps(dispatch) {
  return {
    authActions: bindActionCreators(authActions, dispatch),
    storageActions: bindActionCreators(storageActions, dispatch),
    productActions: bindActionCreators(productActions, dispatch),
    productStorageQuantityActions: bindActionCreators(productStorageQuantityActions, dispatch)
  }
}

const statics = {
  path: 'product',
  menuGroup: 'product',
  breadcrumb: [{
    title: '产品中心'
  }, {
    title: '产品库存管理'
  }]
};

ProductStorageQuantity = createForm()(ProductStorageQuantity);

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(ProductStorageQuantity))
