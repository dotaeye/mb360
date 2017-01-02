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
          <Col span={8} style={{textAlign:'left'}}>
            <FormItem
              {...formItemLayout}
              label="名称"
              >
              {getFieldDecorator('name', {
                  initialValue: ''
                }
              )(
                <Input type="text"/>
              )}
            </FormItem>
          </Col>
          <Col span={8} style={{textAlign:'left'}}>
            <FormItem
              {...formItemLayout}
              label="类别"
              >
              {getFieldDecorator('categoryId', {
                  initialValue: []
                }
              )(
                <Cascader placeholder='产品类别' options={categoryCascader}/>
              )}
            </FormItem>
          </Col>
          <Col span={8} style={{textAlign:'left'}}>
            <FormItem
              {...formItemLayout}
              label="品牌"
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
        <div style={{marginBottom:'24px'}}>

          {getFieldDecorator('isFeaturedProduct', {
            initialValue: '',
          })(
            <Checkbox>热销产品</Checkbox>
          )}
          {getFieldDecorator('isAgreeActive', {
            initialValue: '',
          })(
            <Checkbox>盟友专享</Checkbox>
          )}
          {getFieldDecorator('isVipAlbum', {
            initialValue: '',
          })(
            <Checkbox>尊享专辑</Checkbox>
          )}
          {getFieldDecorator('isMatchAllCar', {
            initialValue: '',
          })(
            <Checkbox>全匹配</Checkbox>
          )}
          {getFieldDecorator('published', {
            initialValue: '',
          })(
            <Checkbox>已上架</Checkbox>
          )}

          <Button type="primary" size="large" icon="search" htmlType="submit" style={{float:'right'}}>搜索</Button>
        </div>
      </Form>
    )
  }

});

ProductSearch = createForm()(ProductSearch);

export default ProductSearch;