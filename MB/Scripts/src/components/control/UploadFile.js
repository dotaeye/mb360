import React,{PropTypes} from 'react';
import { Upload , Icon} from 'antd';
import { guid } from '../../utils/biz';
import _ from 'lodash';

const UploadFile = React.createClass({

  propTypes: {
    multiple: PropTypes.bool,
    value: PropTypes.string,
    origin: PropTypes.bool,//只上传原图
    min: PropTypes.bool,
    mid: PropTypes.bool,
    max: PropTypes.bool
  },

  getDefaultProps(){
    return {
      multiple: false,
      origin: false,//只上传原图
      min: true,
      mid: true,
      max: true
    }
  },
  getInitialState() {
    let fileList = [];
    const { value }=this.props;
    const defaultUrls = value ? value.split(',') : [];
    if (defaultUrls.length > 0) {
      fileList = defaultUrls.map(url=> {
        return {
          uid: guid(),
          name: 'xxx',
          status: 'done',
          url
        }
      })
    }
    return {
      fileList
    };
  },

  onRemove(file){
    let {fileList}=this.state;
    fileList = fileList.filter(f=>f.url !== file.url);
    this.setState({fileList}, ()=> {
      let url = fileList.map(f=>f.url).join(',');
      this.props.onChange(url)
    });
  },

  onUploadChange(info){
    const { origin }=this.props;
    let fileList = info.fileList;
    // 1. 上传列表数量的限制
    //    只显示最近上传的一个，旧的会被新的顶掉
    fileList = fileList.slice(-1);
    // 2. 读取远程路径并显示链接
    fileList = fileList.map((file) => {
      if (file.response) {
        // 组件会将 file.url 作为链接进行展示
        file.url = origin ? file.response.imageUrl : file.response.imageUrl_120;
      }
      return file;
    });
    // 3. 按照服务器返回信息筛选成功上传的文件
    fileList = fileList.filter((file) => {
      if (file.response) {
        return file.response.status === 'success';
      }
      return true;
    });
    this.setState({fileList}, ()=> {
      let url = fileList.map(f=>f.url).join(',');
      this.props.onChange(url)
    });
  },

  render(){
    const {multiple, origin, min ,mid, max }=this.props;
    const queryStr = `?origin=${origin}&min=${min}&mid=${mid}&max=${max}`;
    const props = {
      action: `/api/upload${queryStr}`,
      listType: 'picture-card',
      multiple: multiple,
      onChange: this.onUploadChange
    };
    return (
      <Upload {...props} fileList={this.state.fileList}>
        <Icon type="plus"/>

        <div className="ant-upload-text">上传图片</div>
      </Upload>
    )
  }
});

export default UploadFile;