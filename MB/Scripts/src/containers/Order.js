import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux'
import { Spin, Table , Icon, Button, Modal, Tag, Form, Input, Checkbox, message,Select,Popconfirm } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as authActions from '../actions/auth'
import * as orderActions from '../actions/order'
import orderStatus from '../configs/orderStatus';
import OrderSearch from '../components/form/OrderSearch'
import _ from 'lodash';
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;
const Option = Select.Option;

var Order = React.createClass({

  displayName: 'Order',

  getInitialState() {
    return {
      pagination: {
        pageSize: 10,
        current: 1
      },
      search: {}
    };
  },

  fetchData(option = {}){
    const {pagination,search}=this.state;
    this.props.orderActions.getAll(Object.assign({}, {
      results: pagination.pageSize,
      page: pagination.current,
      orderNo: search.orderNo,
      orderStatusId: search.orderStatusId
    }, option));
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
    this.fetchData({
      results: pagination.pageSize,
      page: pagination.current,
      sortField: sorter.field,
      sortOrder: sorter.order
    });
  },

  onSearchSubmit(formdata){
    this.setState({
      search: formdata
    }, ()=> {
      this.fetchData(Object.assign({}, {page: 1}))
    })
  },

  onConfirm(){

  },

  render() {
    const columns = [{
      title: '订单号',
      dataIndex: 'outTradeNo'
    }, {
      title: '状态',
      dataIndex: 'orderStatusId',
      render: (orderStatusId)=> {
        const item=orderStatus.find(x=>x.value == orderStatusId);
        return <Tag color={item.color}>{item.name}</Tag>
      }
    }, {
      title: '总价',
      dataIndex: 'orderTotal',
      render: (orderTotal)=> {
        return <div style={{color:'red'}}> ¥ {orderTotal}</div>
      }
    }, {
      title: '订单商品',
      dataIndex: 'shopCartItems',
      render: (shopCartItems)=> {
        return shopCartItems.map((shopCart, sIndex)=> {
          return (
            <div key={sIndex} data-flex="main:left cross:center" style={{marginBottom:'10px'}}>
              <img style={{width:'40px'}} src={'http://www.lm123.cc/'+shopCart.imageUrl}/>
              <div>{shopCart.name} {shopCart.attributesXml} X{shopCart.quantity}</div>
            </div>
          )
        });
      }
    }, {
      title: '收货信息',
      dataIndex: 'addressDTO',
      render: (address) => {
        return <div>
          <div>{address.name} {address.phoneNumber} </div>
          <p>{address.province}{address.area}{address.county}{address.detail}</p>
        </div>

      }
    }, {
      title: '操作',
      key: 'operation',
      render: (text, record) => (
        <span>
          <Popconfirm title="是否确认发货?" onConfirm={this.onConfirm(record.id)}>
            <Button type="ghost" shape="circle" icon="check" size="small" title='确认发货'/>
          </Popconfirm>
          <span className="ant-divider"></span>
          <Button type="ghost" shape="circle" icon="exception" size="small" title='打印发货单'/>
        </span>
      )
    }];

    const { order:{ loading, list}} = this.props;
    const data = list ? list.data : [];
    const pagination = Object.assign({}, this.state.pagination, {total: list ? list.recordCount : 0});

    return (
      <div className='container'>
        <div className='ant-list-header'>
          <OrderSearch
            ref='search'
            onSearchSubmit={this.onSearchSubmit}
            />
        </div>
        <Table
          ref='table'
          columns={columns}
          rowKey={record => record.outTradeNo}
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
    order: state.order
  }
}

function mapDispatchToProps(dispatch) {
  return {
    authActions: bindActionCreators(authActions, dispatch),
    orderActions: bindActionCreators(orderActions, dispatch)
  }
}

const statics = {
  path: 'order',
  menuGroup: 'core',
  breadcrumb: [{
    title: '核心业务'
  }, {
    title: '订单管理'
  }]
};

Order = createForm()(Order);

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(Order))
