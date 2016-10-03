import React, { Component } from 'react';
import { Spin, Table, Icon, Button, Modal, Form, Input, Checkbox, message,Select, InputNumber } from 'antd';
import _ from 'lodash';
import ImageListSelect from '../control/ImageListSelect';
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;

var ProductAttributeValueForm = React.createClass({


  clearFields(){
    this.props.form.resetFields();
  },



  render(){
    const { product, entity, imageUrl }=this.props;
    const formItemLayout = {
      labelCol: {span: 4},
      wrapperCol: {span: 20}
    };
    const { getFieldDecorator } = this.props.form;
    return (
      <Form horizontal>
        <FormItem
          {...formItemLayout}
          label="名称"
          >
          {getFieldDecorator('name', {
              initialValue: entity.name,
              rules: [{required: true, message: '请输入名称'}]
            }
          )(
            <Input type="text"/>
          )}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="价格调整"
          >
          {getFieldDecorator('priceAdjustment', {
              initialValue: entity.priceAdjustment
            }
          )(
            <InputNumber step={0.01} type="text"/>
          )}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="选择图片"
          >
          {getFieldDecorator('imageUrl', {
              initialValue: entity.imageUrl
            }
          )(
            <ImageListSelect images={product.imageUrl?product.imageUrl.split(','):[]}/>
          )}
        </FormItem>
      </Form>
    )
  }
});

ProductAttributeValueForm = createForm()(ProductAttributeValueForm);

export default  ProductAttributeValueForm;