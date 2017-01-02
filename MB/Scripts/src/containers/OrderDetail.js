import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { Link } from 'react-router'
import { connect } from 'react-redux'
import moment from 'moment';
import { Spin, Table , Icon, Button, Modal, Tag, Form, Input, Checkbox, message,Select,Popconfirm } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as orderActions from '../actions/order'
import orderStatus from '../configs/orderStatus';
import OrderSearch from '../components/form/OrderSearch'

var OrderDetail = React.createClass({

  displayName: 'OrderDetail',

  fetchData(){
    const {params}=this.props;
    this.props.orderActions.getById(params.id);
  },

  componentDidMount(){
    this.fetchData();
  },

  render() {
    const {entity}=this.props;
    return (
      <div className='container'>
        <div className='ant-list-header' data-flex="main:justify">
          <div className='ant-list-header-left'>
            <Link to='order'>
              <Icon type='arrow-left'/> 返回列表
            </Link>
          </div>
        </div>
        {entity && entity.code == 0 && (
          <div className="ant-table ant-table-large ant-table-bordered">
            <div className="ant-table-body ant-table-bordered">
              <table>
                <thead className="ant-table-thead">
                <tr>
                  <th colSpan="2">订单详情<span style={{"float":"right"}}>下单时间： {moment(entity.createTime).format("YYYY-MM-DD HH:mm:ss")}</span></th>
                </tr>
                </thead>
                <tbody className="ant-table-tbody">
                <tr className="ant-table-row">
                  <td>订单号</td>
                  <td>{entity.data.outTradeNo}</td>
                </tr>
                {entity.data.shopCartItems.map((shopCart, index)=> {
                  return (
                    <tr className="ant-table-row" key={index}>
                      {index == 0 && (
                        <td rowSpan={entity.data.shopCartItems.length}>订单商品</td>
                      )}
                      <td>
                        <div data-flex="main:left cross:center" style={{marginBottom:'10px'}}>
                          <img style={{width:'40px'}} src={'http://www.lm123.cc/'+shopCart.imageUrl}/>

                          <div>{shopCart.name} {shopCart.attributesXml} X{shopCart.quantity}</div>
                        </div>
                      </td>
                    </tr>
                  )
                })}

                <tr className="ant-table-row">
                  <td>收货人</td>
                  <td>
                    <div>{entity.data.addressDTO.name} {entity.data.addressDTO.phoneNumber} </div>
                    <p>{entity.data.addressDTO.province}{entity.data.addressDTO.area}{entity.data.addressDTO.county}{entity.data.addressDTO.detail}</p>
                  </td>
                </tr>

                <tr className="ant-table-row">
                  <td>总金额</td>
                  <td>
                    <div style={{color:'red'}}> ¥ {entity.data.orderTotal}</div>
                  </td>
                </tr>

                </tbody>
              </table>
            </div>
          </div>
        )}
      </div>
    );
  }
});

function mapStateToProps(state) {
  return {
    entity: state.order.entity
  }
}

function mapDispatchToProps(dispatch) {
  return {
    orderActions: bindActionCreators(orderActions, dispatch)
  }
}

const statics = {
  path: 'order',
  menuGroup: 'core',
  breadcrumb: [{
    title: '核心业务'
  }, {
    title: '订单详情'
  }]
};

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(OrderDetail))
