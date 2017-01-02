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
      hasLoad: false,
      loading: true,
      selectProductIds:[]
    };
  },

  fetchData(option){
    const page = (option && option.page) || this.state.pagination.current;
    const promise = [];
    if (!this.state.hasLoad) {
      promise.push(this.props.categoryActions.getCascader());
      promise.push(this.props.manufacturerActions.getSelectList());
    }
    Promise.all(promise).then(err=> {
      const params = Object.assign({
        results: this.state.pagination.pageSize,
        page: page
      }, option);
      this.props.productActions.getAll(params).then(err=> {
        this.setState({
          loading: false,
          hasLoad: true
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
              page: _.max([page - 1, 1])
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
      search: formdata
    }, ()=> {
      this.fetchData(Object.assign({}, {page: 1}, formdata))
    })
  },

  onSetProductStatus(statusType){
    const {selectProductIds}=this.state;
    if(selectProductIds.length==0){
      message.error('请至少选择一条记录');
      return;
    }
    this.props.productActions
      .updateStatus({ids:selectProductIds, statusType})
      .then(err=> {
        if (err) {
          message.error(err.message);
        } else {
          message.success('设置状态成功！');
        }
      })
  },

  render() {
    const columns = [{
      title: 'Id',
      dataIndex: 'id',
      sorter: true,
      width: '50px'
    }, {
      title: '名称',
      dataIndex: 'name',
      width: '250px'
    }, {
      title: '盟友专享',
      dataIndex: 'isAgreeActive',
      render: (isAgreeActive)=> {
        return isAgreeActive ? <Icon type="check-circle" style={{'color':'red'}}/> : <Icon type="check-circle-o"/>
      }
    },{
      title: '尊享专辑',
      dataIndex: 'isVipAlbum',
      render: (isVipAlbum)=> {
        return isVipAlbum ? <Icon type="check-circle" style={{'color':'red'}}/> : <Icon type="check-circle-o"/>
      }
    },{
      title: '通匹配',
      dataIndex: 'isMatchAllCar',
      render: (isMatchAllCar)=> {
        return isMatchAllCar ? <Icon type="check-circle" style={{'color':'red'}}/> : <Icon type="check-circle-o"/>
      }
    },{
      title: '热门',
      dataIndex: 'isFeaturedProduct',
      render: (isFeaturedProduct)=> {
        return isFeaturedProduct ? <Icon type="check-circle" style={{'color':'red'}}/> : <Icon type="check-circle-o"/>
      }
    },{
      title: '上架',
      dataIndex: 'published',
      render: (published)=> {
        return published ? <Icon type="check-circle" style={{'color':'red'}}/> : <Icon type="check-circle-o"/>
      }
    },
      //  {
      //  title: '图片',
      //  dataIndex: 'imageUrl',
      //  render: (url) => url.split(',').map((item, index)=> <img key={index} src={item}
      //                                                           style={{ width:'30px' ,height:'30px',marginRight:'10px'}}/>)
      //},
      //
      {
        title: '价格',
        dataIndex: 'price'
      }, {
        title: 'Vip价格',
        dataIndex: 'vipPrice'
      }, {
        title: '紧急价格',
        dataIndex: 'urgencyPrice'
      },
      {
        title: '操作',
        key: 'operation',
        render: (text, record) => (
          <span>
          <Link to={`product/update/${record.id}`}>
            <Button type="ghost" shape="circle" icon="edit" size="small" title='编辑'/>
          </Link>
            <span className="ant-divider"></span>
          <Link to={`productstoragequantity/${record.id}`}>
            <Button type="ghost" shape="circle" icon="appstore" size="small" title='库存管理'/>
          </Link>
           <span className="ant-divider"></span>
           <Link to={`productcarcate/${record.id}`}>
             <Button type="ghost" shape="circle" icon="pushpin-o" size="small" title='车型匹配'/>
           </Link>
          <span className="ant-divider"></span>
          <Button type="ghost" shape="circle" icon="delete" size="small" title='删除'
                  onClick={this.onRemove.bind(null,record)}/>
        </span>
        )
      }];
    const { cascader }=this.props.category;
    const { selectlist }= this.props.manufacturer;
    const { product:{list }} = this.props;
    const { loading,hasLoad }=this.state;
    const data = list ? list.data : [];
    const pagination = Object.assign({}, this.state.pagination, {total: list ? list.recordCount : 0});

    const rowSelection = {
      onChange: (selectProductIds) => {
        this.setState({
          selectProductIds: selectProductIds
        });
      }
    };
    return (
      <div className='container'>
        {hasLoad && (
          <div className='ant-list-header'>
            <ProductSearchFrom
              ref='search'
              categoryCascader={cascader}
              manufacturerList={selectlist}
              onSearchSubmit={this.onSearchSubmit}
              onSetVipOnly={this.onSetVipOnly}
              />

            <div>
              <Button type="primary" htmlType="button" onClick={this.onSetProductStatus.bind(this,0)}
                      style={{marginRight:'10px',marginBottom:'10px'}}>设置盟友专享</Button>
              <Button type="primary" htmlType="button" onClick={this.onSetProductStatus.bind(this,1)}
                      style={{marginRight:'10px',marginBottom:'10px'}}>取消盟友专享</Button>
              <Button type="primary" htmlType="button" onClick={this.onSetProductStatus.bind(this,2)}
                      style={{marginRight:'10px',marginBottom:'10px'}}>设置尊享专辑</Button>
              <Button type="primary" htmlType="button" onClick={this.onSetProductStatus.bind(this,3)}
                      style={{marginRight:'10px',marginBottom:'10px'}}>取消尊享专辑</Button>
              <Button type="primary" htmlType="button" onClick={this.onSetProductStatus.bind(this,4)}
                      style={{marginRight:'10px',marginBottom:'10px'}}>设置热门</Button>
              <Button type="primary" htmlType="button" onClick={this.onSetProductStatus.bind(this,5)}
                      style={{marginRight:'10px',marginBottom:'10px'}}>取消热门</Button>
              <Button type="primary" htmlType="button" onClick={this.onSetProductStatus.bind(this,6)}
                      style={{marginRight:'10px',marginBottom:'10px'}}>设置通匹配</Button>
              <Button type="primary" htmlType="button" onClick={this.onSetProductStatus.bind(this,7)}
                      style={{marginRight:'10px',marginBottom:'10px'}}>取消通匹配</Button>
              <Button type="primary" htmlType="button" onClick={this.onSetProductStatus.bind(this,8)}
                      style={{marginRight:'10px',marginBottom:'10px'}}>批量上架</Button>
              <Button type="primary" htmlType="button" onClick={this.onSetProductStatus.bind(this,9)}
                      style={{marginRight:'10px',marginBottom:'10px'}}>批量下架</Button>
              <Link to='product/create'>
                <Button type="primary" icon="plus" style={{marginRight:'10px',marginBottom:'10px'}}>添加产品</Button>
              </Link>
            </div>
          </div>
        )}
        <Table
          ref='table'
          columns={columns}
          rowKey={record => record.id}
          dataSource={data}
          pagination={pagination}
          loading={loading}
          rowSelection={rowSelection}
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
