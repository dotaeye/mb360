import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux'
import { Spin, Table, Icon, Button, Modal, Form, Input, InputNumber, Checkbox, message,Select, Cascader  } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as authActions from '../actions/auth'
import * as bannerActions from '../actions/banner'
import { ProfileForm } from '../components';
import { getGroupSelectData , hasError, setCascadeValues} from '../utils/biz';
import UploadFile from '../components/control/UploadFile'
import _ from 'lodash';
const FormItem = Form.Item;
const createForm = Form.create;
const confirm = Modal.confirm;
const Option = Select.Option;

var Department = React.createClass({

  displayName: 'Department',

  getInitialState() {
    return {
      pagination: {
        pageSize: 10,
        current: 1
      }
    };
  },

  fetchData(page){
    this.props.bannerActions.getAll({
      results: this.state.pagination.pageSize,
      page: page || this.state.pagination.current
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
    this.props.bannerActions.getAll({
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
          title: '添加横幅'
        });

  },

  onEdit(record){
    this.props.bannerActions.getById(record.id).then((err)=> {
      if (err) {
        message.error('获取品牌数据失败！请刷新页面尝试。');
      }
      else {
        this.setState({
          visible: true,
          edit: true,
          title: '编辑横幅'
        });
      }
    });
  },

  onRemove(record){
    const self = this;
    const { banner:{ list }} = this.props;
    let source = list.data;
    const remove = this.props.bannerActions.remove;
    confirm({
      title: '确认删除该横幅？',
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
    const { update, create} =this.props.bannerActions;
    const { entity }= this.props.banner;
    this.props.form.validateFields((errors, formdata) => {
      if (!!errors) {
        console.log('Errors in form!!!');
        return;
      }
      if (edit) {
        formdata.id = entity.id;
        //convert the array value to single.
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
    const columns = [{
      title: 'Id',
      dataIndex: 'id',
      width: '20%'
    }, {
      title: 'URL',
      dataIndex: 'url'
    },{
      title: '图片',
      dataIndex: 'imageUrl',
      render: (url) => <img src={url} style={{ width:'30px' ,height:'30px'}}/>
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
    const { banner:{ loading, list, entity }} = this.props;
    const { title, visible, edit }=this.state;
    const data = list ? list.data : [];
    const pagination = Object.assign({}, this.state.pagination, {total: list ? list.recordCount : 0});
    const { getFieldDecorator } = this.props.form;
    const record = edit ? entity : {};

    const formItemLayout = {
      labelCol: {span: 4},
      wrapperCol: {span: 20}
    };
    return (
      <div className='container'>
        <div className='ant-list-header' data-flex="dir:right">
          <div className='ant-list-header-right'>
            <Button type="primary" icon="plus" onClick={this.onAdd}>添加横幅</Button>
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
        <Modal title={title} visible={visible} onOk={this.onModalSubmit}
               onCancel={this.onModalClose}>
          <Form horizontal>
          
            <FormItem
              {...formItemLayout}
              label="URL地址"
              >
              {getFieldDecorator('url', {
                  initialValue: record.url,
                  rules: [{required: true, message: '请输入URL地址'}]
                }
              )(
                <Input  type="text"/>
              )}
            </FormItem>

             {visible && (
              <FormItem
                {...formItemLayout}
                label="分类图片"
                >
                {getFieldDecorator('imageUrl', {
                    initialValue: record.imageUrl
                  }
                )(
                  <UploadFile  origin={true}/>
                )}
              </FormItem>
            )}

           <FormItem
              {...formItemLayout}
              label="Navtie路由"
              >
              {getFieldDecorator('nativeRoute', {initialValue: record.nativeRoute, valuePropName: 'checked'})(
                <Checkbox />
              )}
            </FormItem>
      
            <FormItem
              {...formItemLayout}
              label="排序"
              >
              {getFieldDecorator('displayOrder', {
                  initialValue: record.displayOrder || 0
                }
              )(
                <InputNumber  />
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
    banner: state.banner
  }
}

function mapDispatchToProps(dispatch) {
  return {
    authActions: bindActionCreators(authActions, dispatch),
    bannerActions: bindActionCreators(bannerActions, dispatch)
  }
}

const statics = {
  path: 'banner',
  menuGroup: 'system',
  breadcrumb: [{
    title: '组织架构'
  }, {
    title: '横幅管理'
  }]
};

Department = createForm()(Department);

export default connectStatic(statics)(connect(mapStateToProps, mapDispatchToProps)(Department))

