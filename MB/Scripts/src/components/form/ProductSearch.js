import React, {Component} from 'react';
import {Spin, Table, Icon, Button, Row, Col, Form, Input, InputNumber, Checkbox, message, Select, Cascader} from 'antd';
import _ from 'lodash';
import { Link } from 'react-router';
import { setCascadeValues} from '../../utils/biz';
const FormItem = Form.Item;
const createForm = Form.create;
const Option = Select.Option;


var ProductSearch = React.createClass({

  displayName: 'ProductSearch',

  onSubmit(e){
  	e.preventDefault();
    this.props.form.validateFields((err, formdata) => {
      if (err) {
        return;
      }
      formdata.categoryId = formdata.categoryId[formdata.categoryId.length - 1];
      this.props.onSearchSubmit(formdata);
    });
  },

  render(){
    const {getFieldDecorator} = this.props.form;
    const formItemLayout = {
      labelCol: {span: 5},
      wrapperCol: {span: 19},
    };
    const {categoryCascader, manufacturerList}=this.props;
    return (
      <Form horizontal onSubmit={this.onSubmit} style={{maxWidth:'none'}}>
        <Row gutter={10}>
          <Col span={8}>
            <FormItem
              {...formItemLayout}
              label="产品名称"
            >
              {getFieldDecorator('name', {
                  initialValue: ''
                }
              )(
                <Input type="text"/>
              )}
            </FormItem>
          </Col>
          <Col span={8}>
            <FormItem
              {...formItemLayout}
              label="产品类别"
            >
              {getFieldDecorator('categoryId', {
                  initialValue: []
                }
              )(
                <Cascader placeholder='产品类别' options={categoryCascader} />
              )}
            </FormItem>
          </Col>
          <Col span={8}>
            <FormItem
              {...formItemLayout}
              label="所属品牌"
            >
              {getFieldDecorator('manufacturerId', {
                initialValue: '',
              })(
                <Select placeholder='所属品牌'>
                  <Option key={0}>所有</Option>
                  {manufacturerList.map(item=> {
                    return (
                      <Option key={item.id}>{item.name}</Option>
                    )
                  })}
                </Select>
              )}
            </FormItem>
          </Col>
        </Row>
        <Row>
          <Col span={24} style={{ textAlign: 'right' }}>
            <Button type="primary" icon="search" htmlType="submit" style={{marginRight:'10px'}}>搜索</Button>

            <Link to='product/create'>
              <Button type="primary" icon="plus" >添加产品</Button>
            </Link>

          </Col>
        </Row>
      </Form>
    )
  }

});

ProductSearch = createForm()(ProductSearch);

export default ProductSearch;