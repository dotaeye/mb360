// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router';
import { Spin, Table, Icon, Button, Modal, Form, Input, InputNumber, Checkbox, message,Select, Cascader } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as authActions from '../actions/auth'
import * as carCateActions from '../actions/carCate'
import * as productActions from '../actions/product'
import * as productCarCateActions from '../actions/productCarCate'
import { setCascadeValues, getCascaderName } from '../utils/biz';
import _ from 'lodash';
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;
const Option = Select.Option;

var ProductCarCate = React.createClass({

  displayName: 'ProductCarCate',

  getInitialState() {
    return {
      pagination: {
        pageSize: 1000,
        current: 1
      },
      loading: true
    };
  },

  fetchData(page){
    const {routeParams:{id}}=this.props;
    var promise = [];
    promise.push(this.props.carCateActions.getCascader());
    promise.push(this.props.productActions.getById(id));
    Promise.all(promise).then(err=> {
      this.props.productCarCateActions.getAll({
        id,
        results: this.state.pagination.pageSize,
        page: page || this.state.pagination.current
      }).then(err=>{
        this.setState({
          loading:false
        })
      })
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
    this.props.productCarCateActions.getAll({
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
      title: '添加车型匹配',
      record: {}
    });
  },

  onEdit(record){
    this.props.productCarCateActions.getById(record.id).then((err)=> {
      if (err) {
        message.error('获取车型匹配数据失败！请刷新页面尝试。');
      }
      else {
        this.setState({
          visible: true,
          edit: true,
          title: '编辑车型匹配'
        });
      }
    });
  },

  onRemove(record){
    const self = this;
    const { productCarCate:{ list }} = this.props;
    let source = list.data;
    const remove = this.props.productCarCateActions.remove;
    confirm({
      title: '确认删除该车型匹配？',
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
    const { update, create} =this.props.productCarCateActions;
    const { entity }= this.props.productCarCate;
    this.props.form.validateFields((errors, formdata) => {
      if (!!errors) {
        console.log('Errors in form!!!');
        return;
      }
      formdata.productId = id;
      formdata.carCateId = formdata.carCateId[formdata.carCateId.length - 1];

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
    const cascader = this.props.carCate.cascader;
    const columns = [{
      title: 'Id',
      dataIndex: 'id',
      sorter: true,
      width: '20%'
    }, {
      title: '车型',
      dataIndex: 'carCateId',
      render: (carCateId)=> {
        let cascaderIds = [];
        setCascadeValues(cascader, carCateId, cascaderIds);
        cascaderIds = cascaderIds.reverse();
        let carCates = getCascaderName(cascaderIds, cascader);
        return carCates.join('-');
      }
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
    const { productCarCate:{  list, entity }} = this.props;
    const product = this.props.product.entity;
    const { title, visible, edit, loading }=this.state;
    const data = list ? list.data : [];
    const record = edit ? entity : {};
    const pagination = Object.assign({}, this.state.pagination, {total: list ? list.recordCount : 0})
    const { getFieldDecorator } = this.props.form;

    let defaultValues = [];
    if (record.carCateId) {
      setCascadeValues(cascader, record.carCateId, defaultValues);
      defaultValues = defaultValues.reverse();
    }

    const formItemLayout = {
      labelCol: {span: 4},
      wrapperCol: {span: 20}
    };

    const checkCarCate = (rule, value, callback)=> {
      if (data.filter(item=>item.carCateId == value).length > 0) {
        callback(new Error('当前车型已经添加!'));
      } else {
        callback();
      }
    };

    return (
      <div className='container'>
        <div className='ant-list-header' data-flex="main:justify">

          <div>
            <Link to='product'>
              <Icon type='arrow-left'/> 返回列表
            </Link>
          </div>

          <div className='ant-list-header-right'>
            <Button type="primary" icon="plus"  onClick={this.onAdd}>添加车型匹配</Button>
          </div>
        </div>
        <div className='nav-tabs-container'>
          <ul className="nav nav-tabs">
            <li><Link to={`product/update/${product.id}`}>基本信息</Link></li>
            <li><Link to={`productstoragequantity/${product.id}`}>管理库存</Link></li>
            <li className='active'><a>车型匹配</a></li>
            <li><Link to={`productattributemapping/${product.id}`}>产品属性</Link></li>
            <li><Link to={`productspecificationattribute/${product.id}`}>产品规格</Link></li>
          </ul>
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
              label="选择车型"
              >
              {getFieldDecorator('carCateId', {
                  initialValue: defaultValues,
                  rules: [{
                    required: true,
                    type: 'array',
                    message: '请选择车型'
                  }, {
                    validator: checkCarCate
                  }]
                }
              )(
                <Cascader placeholder='请选择车型' options={cascader}/>
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
    carCate: state.carCate,
    product: state.product,
    productCarCate: state.productCarCate
  }
}

function mapDispatchToProps(dispatch) {
  return {
    authActions: bindActionCreators(authActions, dispatch),
    carCateActions: bindActionCreators(carCateActions, dispatch),
    productActions: bindActionCreators(productActions, dispatch),
    productCarCateActions: bindActionCreators(productCarCateActions, dispatch)
  }
}

const statics = {
  path: 'productcarcate',
  menuGroup: 'product',
  breadcrumb: [{
    title: '产品中心'
  }, {
    title: '车型匹配管理'
  }]
};

ProductCarCate = createForm()(ProductCarCate);

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(ProductCarCate))
