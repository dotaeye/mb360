import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux'
import { Link } from 'react-router';
import { Spin, Table, Icon, Row, Col, Button, Modal, Form, Input, Checkbox, message,Select } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as authActions from '../actions/auth'
import * as productActions from '../actions/product'
import * as categoryActions from '../actions/category'
import * as manufacturerActions from '../actions/manufacturer'
import ProductSearchFrom from '../components/form/ProductSearch'

import _ from 'lodash';
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;
const Option = Select.Option;

var Product = React.createClass({

  displayName: 'Product',

  getInitialState() {
    return {
      pagination: {
        pageSize: 10,
        current: 1
      },
      hasLoad:false,
      loading:true
    };
  },

  fetchData(option){
    const page= (option&&option.page)||this.state.pagination.current;
    const promise = [];
    if(!this.state.hasLoad){
      promise.push(this.props.categoryActions.getCascader());
      promise.push(this.props.manufacturerActions.getSelectList());
    }
    Promise.all(promise).then(err=> {
      const params=Object.assign({
        results: this.state.pagination.pageSize,
        page: page
      },option);
      this.props.productActions.getAll(params).then(err=>{
        this.setState({
          loading:false,
          hasLoad:true
        })
      });
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
    this.props.productActions.getAll({
      results: pagination.pageSize,
      page: pagination.current,
      sortField: sorter.field,
      sortOrder: sorter.order
    });
  },

  onRemove(record){
    const self = this;
    const { product:{ list }} = this.props;
    let source = list.data;
    const remove = this.props.productActions.remove;
    confirm({
      title: '确认删除该Product？',
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
            self.fetchData({
              page:_.max([page - 1, 1])
            });
          }
        });
      },
      onCancel() {
      }
    });
  },


  onSearchSubmit(formdata){
    this.setState({
      search:formdata
    },()=>{
      this.fetchData(Object.assign({},{page:1},formdata))
    })
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
    },{
      title: 'SKU',
      dataIndex: 'sku'
    },{
      title: '图片',
      dataIndex: 'imageUrl',
      render: (url) => url.split(',').map((item,index)=> <img key={index} src={item} style={{ width:'30px' ,height:'30px',marginRight:'10px'}}/>) 
    },{
      title: '价格',
      dataIndex: 'price'
    },{
      title: 'Vip价格',
      dataIndex: 'vipPrice'
    },{
      title: '紧急调配价格',
      dataIndex: 'urgencyPrice'
    },
     {
      title: '操作',
      key: 'operation',
      render: (text, record) => (
        <span>
          <Link to={`product/update/${record.id}`}>
            <Button type="ghost" shape="circle" icon="edit" size="small" title='编辑' />
          </Link>
            <span className="ant-divider"></span>
          <Link to={`productstoragequantity/${record.id}`}>
            <Button type="ghost" shape="circle" icon="appstore" size="small" title='库存管理' />
          </Link>
           <span className="ant-divider"></span>
           <Link to={`productcarcate/${record.id}`}>
            <Button type="ghost" shape="circle" icon="pushpin-o" size="small" title='车型匹配' />
          </Link>
          <span className="ant-divider"></span>
          <Button type="ghost" shape="circle" icon="delete" size="small" title='删除'
                  onClick={this.onRemove.bind(null,record)}/>
        </span>
      )
    }];
    const { cascader }=this.props.category;
    const { selectlist }= this.props.manufacturer;
    const { product:{list, entity }} = this.props;
    const { title, visible, edit, loading,hasLoad }=this.state;
    const data = list ? list.data : [];
    const pagination = Object.assign({}, this.state.pagination, {total: list ? list.recordCount : 0})
    const { getFieldDecorator } = this.props.form;
    const record = edit ? entity : {};
    const formItemLayout = {
      labelCol: {span: 4},
      wrapperCol: {span: 20}
    };
    return (
      <div className='container'>
         {hasLoad&&(
          <div className='ant-list-header'>
            <ProductSearchFrom  
              ref='search'
              categoryCascader={cascader}
              manufacturerList={selectlist}
              onSearchSubmit={this.onSearchSubmit}
              />
          </div>
          )}
        <Table
          ref='table'
          columns={columns}
          rowKey={record => record.id}
          dataSource={data}
          pagination={pagination}
          loading={loading}
          onChange={this.handleTableChange}
          />
      </div>
    );
  }
});

function mapStateToProps(state) {
  return {
    auth: state.auth,
    product: state.product,
    category: state.category,
    manufacturer: state.manufacturer
  }
}

function mapDispatchToProps(dispatch) {
  return {
    authActions: bindActionCreators(authActions, dispatch),
    productActions: bindActionCreators(productActions, dispatch),
    categoryActions: bindActionCreators(categoryActions, dispatch),
    manufacturerActions: bindActionCreators(manufacturerActions, dispatch)
  }
}

const statics = {
  path: 'product',
  menuGroup: 'product',
  breadcrumb: [{
    title: '产品中心'
  }, {
    title: '产品列表'
  }]
};

Product = createForm()(Product);

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(Product))
