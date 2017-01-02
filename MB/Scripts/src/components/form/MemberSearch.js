import React, {Component} from 'react';
import {Spin, Table, Icon, Button, Row, Col, Form, Input, InputNumber, Checkbox, message, Select, Cascader} from 'antd';
import _ from 'lodash';
import { Link } from 'react-router';
import orderStatus from '../../configs/orderStatus';
const FormItem = Form.Item;
const createForm = Form.create;
const Option = Select.Option;


var MemberSearch = React.createClass({

  displayName: 'MemberSearch',

  onSubmit(e){
    e.preventDefault();
    this.props.form.validateFields((err, formdata) => {
      if (err) {
        return;
      }
      this.props.onSearchSubmit(formdata);
    });
  },

  render(){
    const {getFieldDecorator} = this.props.form;
    const formItemLayout = {
      labelCol: {span: 5},
      wrapperCol: {span: 19},
    };

    return (
      <Form horizontal onSubmit={this.onSubmit} style={{maxWidth:'none'}}>
        <Row gutter={10}>
          <Col span={8}>
            <FormItem
              {...formItemLayout}
              label="手机号"
              >
              {getFieldDecorator('orderNo', {
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
              label="会员状态"
              >
              {getFieldDecorator('orderStatusId', {
                initialValue: ''
              })(
                <Select placeholder='订单状态'>
                  <Option key={0}>所有</Option>
                  {orderStatus.map(item=> {
                    return (
                      <Option key={item.value}>{item.name}</Option>
                    )
                  })}
                </Select>
              )}
            </FormItem>
          </Col>
          <Col span={8}>
            <Button type="primary" icon="search" size="large" htmlType="submit" style={{marginRight:'10px'}}>搜索</Button>
          </Col>
        </Row>
      </Form>
    )
  }

});

MemberSearch = createForm()(MemberSearch);

export default MemberSearch;