import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux'
import { Link } from 'react-router';
import { Spin, Table, Icon, Button, Modal, Form, Input, Checkbox, message,Select } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as authActions from '../actions/auth'
import * as memberActions from '../actions/member'
import moment from 'moment';
import 'moment/locale/zh-cn'
moment.locale('zh-cn');
import _ from 'lodash';
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;
const Option = Select.Option;

var Member = React.createClass({

  displayName: 'Member',

  getInitialState() {
    return {
      pagination: {
        pageSize: 10,
        current: 1
      }
    };
  },

  fetchData(page){
    this.props.memberActions.getAll({
      results: this.state.pagination.pageSize,
      page: page || this.state.pagination.current
    });
  },

  componentDidMount(){
    this.fetchData();
  },

  onRemove(record){
    const self = this;
    const { member:{ list }} = this.props;
    let source = list.data;
    const remove = this.props.memberActions.remove;
    confirm({
      title: '确认删除该会员？',
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

  handleTableChange(pagination, filters, sorter) {
    const pager = this.state.pagination;
    pager.current = pagination.current;
    this.setState({
      pagination: pager
    });
    this.props.memberActions.getAll({
      results: pagination.pageSize,
      page: pagination.current,
      sortField: sorter.field,
      sortOrder: sorter.order
    });
  },
  render() {
    const columns = [{
      title: '用户名',
      dataIndex: 'userName'
    },{
      title: '手机',
      dataIndex: 'phoneNumber'
    },  {
      title: '创建时间',
      dataIndex: 'createTime',
      render: (createTime)=> {
        return  createTime ? moment(createTime).startOf('minutes').fromNow() : ''
      }
    },{
      title: '最后登陆',
      dataIndex: 'lastLoginTime',
      render: (time)=> {
        return time ? moment(time).startOf('minutes').fromNow() : ''
      }
    }, {
      title: '操作',
      key: 'operation',
      render: (text, record) => (
        <span>
          <Link to={`member/update/${record.id}`}>
            <Button type="ghost" shape="circle" icon="edit" size="small" title='编辑'/>
          </Link>
          <span className="ant-divider"></span>
          <Button type="ghost" shape="circle" icon="delete" size="small" title='删除'
                  onClick={this.onRemove.bind(null,record)}/>
        </span>
      )
    }];
    const { member:{ loading, list }} = this.props;
    const data = list ? list.data : [];
    const pagination = Object.assign({}, this.state.pagination, {total: list ? list.recordCount : 0});
    return (
      <div className='container'>
        <div className='ant-list-header' data-flex="dir:right">
          <div className='ant-list-header-right'>
            <Link to='member/create'>
              <Button type="primary" icon="plus">添加会员</Button>
            </Link>
          </div>
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
      </div>
    );
  }
});

function mapStateToProps(state) {
  return {
    auth: state.auth,
    member: state.member
  }
}

function mapDispatchToProps(dispatch) {
  return {
    authActions: bindActionCreators(authActions, dispatch),
    memberActions: bindActionCreators(memberActions, dispatch)
  }
}

const statics = {
  path: 'member',
  menuGroup: 'core',
  breadcrumb: [{
    title: '用户中心'
  }, {
    title: '用户列表'
  }]
};

Member = createForm()(Member);

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(Member))
