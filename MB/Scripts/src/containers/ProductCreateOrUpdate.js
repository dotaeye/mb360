// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { Link } from 'react-router';
import { connect } from 'react-redux'
import { Alert, Spin, Table, Icon, Button, Modal, Form, Input, InputNumber ,Checkbox, Tabs, message,Select, Cascader } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as authActions from '../actions/auth'
import * as productActions from '../actions/product'
import * as categoryActions from '../actions/category'
import * as storageActions from '../actions/storage'
import * as carCateActions from '../actions/carCate'
import * as manufacturerActions from '../actions/manufacturer'
import _ from 'lodash';
import { setCascadeValues } from '../utils/biz'; 
import UploadFile from '../components/control/UploadFile'
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;
const Option = Select.Option;
const TabPane = Tabs.TabPane;

var ProductCreateOrUpdate = React.createClass({

  displayName: 'ProductCreateOrUpdate',

  fetchData(){
    const { routeParams:{id} } =this.props;
    const promise = [];
    promise.push(this.props.categoryActions.getCascader());
    promise.push(this.props.manufacturerActions.getSelectList());
    Promise.all(promise).then(err=>{
        if(id){
           this.props.productActions.getById(id)
         }
    });
  },

  getInitialState(){
    return {
      editMode:true
    }
  },

  componentDidMount(){
    this.fetchData();
  },


  onModalSubmit(){
    const { routeParams:{id} }=this.props;
    const { update, create} =this.props.productActions;
    const { entity }= this.props.product;
    this.props.form.validateFields((errors, formdata) => {
      if (!!errors) {
        console.log('Errors in form!!!');
        return;
      }
      formdata.categoryId = formdata.categoryId[formdata.categoryId.length - 1];
      if (id) {
        formdata.id = entity.id;
        update(formdata).then((err)=> {
          if (err) {
            message.error('更新数据失败。');
          } else {
            message.success('更新数据成功！');
            this.setState({
              editMode:false
            })
          }
        });
      } else {
        create(formdata).then((err)=> {
          if (err) {
            message.error('创建数据失败。');
          } else {
            message.success('创建数据成功！');
            this.setState({
              editMode:false
            })
          }
        });
      }
    });
  },

  /*
  *  <div className='nav-tabs-container'>
          <ul className="nav nav-tabs">
            <li className="active"><a>Product info</a></li>
            <li><a href="#tab-seo">SEO</a></li>
          </ul>
        </div>
  */
  render() {
    const { product:{ loading, entity }, routeParams:{id}} = this.props;
    const { cascader }=this.props.category;
    const { getFieldProps } = this.props.form;
    const record = id ? entity : {};
    const { editMode }= this.state;
    const { selectlist }= this.props.manufacturer;
    let categoryValues = [];
    if (record.categoryId) {
      setCascadeValues(cascader, record.categoryId, categoryValues);
      categoryValues = categoryValues.reverse();
    }
    const formItemLayout = {
      labelCol: {span: 4},
      wrapperCol: {span: 20}
    };
    return (
      <div className='container'>
        <div className='ant-list-header' data-flex="main:justify">
         <div className='ant-list-header-left'>
            <Link to='product'>
              <Icon type='arrow-left'/> 返回列表
            </Link>
          </div>
          {editMode&&(
            <div className='ant-list-header-right'>
              <Button type="primary" icon="save" onClick={this.onModalSubmit}>保存</Button>
            </div>  
            )}
        </div>
      
        {editMode&&(
          <Form horizontal>

                <FormItem
                  {...formItemLayout}
                  label="名称"
                  >
                  <Input  {...getFieldProps('name', {
                      initialValue: record.name,
                      rules: [{required: true, message: '请输入名称'}]
                    }
                  )} type="text"/>
                </FormItem>

                 <FormItem
                  {...formItemLayout}
                  label="产品分类"
                  >
                  <Cascader {...getFieldProps('categoryId', {
                      initialValue: categoryValues,
                      rules: [{required: true, type:'array', message: '请选择产品分类'}]
                    }
                  )} placeholder='请选择产品分类' options={cascader} />
                </FormItem>

                <FormItem
                  {...formItemLayout}
                  label="所属品牌"
                  >
                  <Select {...getFieldProps('manufacturerId', {
                      initialValue: record.manufacturerId?`${record.manufacturerId}`:'',
                      rules: [{required: true, message: '请选择所属品牌'}]
                    }
                  )} placeholder='请选择所属品牌' >
                    {selectlist.map(item=>{
                      return (
                        <Option key={item.id} >{item.name}</Option>
                      )
                    })}
                  </Select>
                </FormItem>

                <FormItem
                  {...formItemLayout}
                  label="热销产品"
                  >
                  <Checkbox {...getFieldProps('isFeaturedProduct', {initialValue: record.isFeaturedProduct, valuePropName: 'checked'})}/>
                </FormItem>

                <FormItem
                  {...formItemLayout}
                  label="SKU"
                  >
                  <Input  {...getFieldProps('sku', {
                      initialValue: record.sku,
                      rules: [{required: true, message: '请输入SKU'}]
                    }
                  )} type="text"/>
                </FormItem>

                <FormItem
                  {...formItemLayout}
                  label="产品价格"
                  >
                  <InputNumber step={0.01}  {...getFieldProps('price', {
                      initialValue: record.price,
                      rules: [{required: true, type:'number', message: '请输入产品价格'}]
                    }
                  )} type="text"/>
                </FormItem>

                <FormItem
                  {...formItemLayout}
                  label="VIP价格"
                  >
                  <InputNumber step={0.01}  {...getFieldProps('vipPrice', {
                      initialValue: record.vipPrice,
                      rules: [{required: true, type:'number', message: '请输入VIP价格'}]
                    }
                  )} type="text"/>
                </FormItem>

                <FormItem
                  {...formItemLayout}
                  label="紧急调配价格"
                  >
                  <InputNumber step={0.01}  {...getFieldProps('urgencyPrice', {
                      initialValue: record.urgencyPrice,
                      rules: [{required: true, type:'number', message: '请输入紧急调配价格'}]
                    }
                  )} type="text"/>
                </FormItem>

                <FormItem
                  {...formItemLayout}
                  label="产品描述"
                  >
                  <Input  {...getFieldProps('description', {
                      initialValue: record.description,
                      rules: [{required: true, message: '请输入产品描述'}]
                    }
                  )} type="textarea"/>
                </FormItem>

                {!loading&&(
                  <FormItem
                    {...formItemLayout}
                    label="产品图片"
                    >
                    <UploadFile  {...getFieldProps('imageUrl', {
                        initialValue: record.imageUrl,
                        rules: [{required: true, message: '请上传产品图片'}]
                      }
                    )}  multiple={true} />

                  </FormItem>
                )}

                {!loading&&(
                  <FormItem
                    {...formItemLayout}
                    label="详情图片"
                    >
                    <UploadFile  {...getFieldProps('detailUrl', {
                        initialValue: record.detailUrl,
                        rules: [{required: true, message: '请上传详情图片'}]
                      }
                    )}  multiple={true} />

                  </FormItem>
                )}


                <FormItem
                  {...formItemLayout}
                  label="参与团购活动"
                  help="如果同意将产品加入到团购活动中"
                  >
                  <Checkbox {...getFieldProps('isAgreeActive', {initialValue: record.isAgreeActive, valuePropName: 'checked'})}/>
                </FormItem>
       
          </Form>
          )}

          {!editMode&&(
            <div>

              <Alert message="产品信息保存成功！" type="success" showIcon />

              <div data-flex="main:center">

                <Link to={`productstoragequantity/${entity.id}`} >

                <Button type='primary'  style={{marginRight:'30px'}}>管理库存</Button>

                </Link>

                <Link to={`productcarcate/${entity.id}`} >

                <Button type='primary' >车型匹配</Button>

                </Link>  
              </div>
            </div>
            )}
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
  path: 'product/create',
  menuGroup: 'product',
  breadcrumb: [{
    title: '产品中心'
  }, {
    title: '产品添加'
  }]
};

ProductCreateOrUpdate = createForm()(ProductCreateOrUpdate);

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(ProductCreateOrUpdate))
