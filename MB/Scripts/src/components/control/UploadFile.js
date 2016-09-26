import React,{PropTypes} from 'react';
import { Upload , Icon, Modal, message} from 'antd';
import { guid } from '../../utils/biz';
import _ from 'lodash';

const UploadFile = React.createClass({

  propTypes: {
    multiple: PropTypes.bool,
    value: PropTypes.string,
    origin: PropTypes.bool,//只上传原图
    min: PropTypes.bool,
    mid: PropTypes.bool,
    max: PropTypes.bool,
    onlyImage : PropTypes.bool
  },

  getDefaultProps(){
    return {
      multiple: false,
      origin: false,//只上传原图
      min: true,
      mid: true,
      max: true,
      onlyImage: true
    }
  },
  getInitialState() {
 	return this.getDefaultStateFromProps(this.props);
  },

  getDefaultStateFromProps(props){
   let fileList = [];
    const { value }=props;
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

  //componentWillReceiveProps(newProps){
  //  console.log('componentWillReceiveProps')
  //	// this.setState(this.getDefaultStateFromProps(newProps));
  //},

  onRemove(file){
    let {fileList}=this.state;
    fileList = fileList.filter(f=>f.url !== file.url);
    this.setState({fileList}, ()=> {
      let url = fileList.map(f=>f.url).join(',');
      this.props.onChange(url)
    });
  },

  onUploadChange(info){
    console.log(arguments)
    const { origin, multiple }=this.props;
    let fileList = info.fileList;
    // 1. 上传列表数量的限制
    //    只显示最近上传的一个，旧的会被新的顶掉
    if(!multiple){
    	fileList = fileList.slice(-1);
    }

    // 2. 读取远程路径并显示链接
    fileList = fileList.map((file) => {
      if (file.response) {
        // 组件会将 file.url 作为链接进行展示
        file.url = origin ? file.response.imageUrl : file.response.imageUrl_120;
        file.originUrl=file.response.imageUrl;
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

  onMoveLeft(index,disabled){
  	let {fileList}=this.state;
  	if(disabled) return;
  	fileList=fileList.slice(0,index-1)
  	.concat(fileList.slice(index,index+1))
  	.concat(fileList.slice(index-1,index))
  	.concat(fileList.slice(index+1))
    this.setState({fileList}, ()=> {
      let url = fileList.map(f=>f.url).join(',');
      this.props.onChange(url)
    });
  },

  onMoveRight(index,disabled){
  	let {fileList}=this.state;
  	if(disabled) return;
  	fileList=fileList.slice(0,index)
  	.concat(fileList.slice(index+1,index+2))
  	.concat(fileList.slice(index,index+1))
  	.concat(fileList.slice(index+2))
    this.setState({fileList}, ()=> {
      let url = fileList.map(f=>f.url).join(',');
      this.props.onChange(url)
    });
  },

  onPreview(file){
  	this.setState({
  		priviewVisible:true,
  		priviewImage:file.originUrl||file.url
  	})
  },

  handleCancel() {
    this.setState({
      priviewVisible: false,
    });
  },

  render(){
    const {multiple, origin, min ,mid, max, onlyImage }=this.props;
    const { fileList }=this.state;
    const queryStr = `?origin=${origin}&min=${min}&mid=${mid}&max=${max}`;
    const props = {
      action: `/api/upload${queryStr}`,
      listType: 'picture-card',
      multiple: multiple,
      onChange: this.onUploadChange,
      beforeUpload(file){
      	if(onlyImage){
	      	const isImage = ['image/jpeg','image/png'].includes(file.type) ;
		    if (!isImage) {
		      message.error('只能上传 JPG,PNG 文件哦！');
		    }
		    return isImage;
      	}
      	return true;
      }
    };

    return (
    	<div className='upload-container'>
    		{fileList.map((file,index)=>{
    			let originUrl=file.originUrl||file.url;
    			let minUrl=origin?originUrl:file.url;
    			let disableLeft=index===0;
    			let disableRight=index===this.state.fileList.length-1;

    			return (
		    		<div key={index} className="upload-item">
			          <img src={minUrl} />
			          {multiple&&(
     					<span className='upload-tool top'>
			          		<Icon className={disableLeft?'disabled':''} 
			          			type='caret-left'
			          			onClick={this.onMoveLeft.bind(this,index,disableLeft)}/>
			            	<Icon className={disableRight?'disabled':''} 
			            		type='caret-right' 
			            		onClick={this.onMoveRight.bind(this,index,disableRight)}/>
			           	</span>
			          )}
			           <span className='upload-tool bottom'>
			          	<Icon type='delete' onClick={this.onRemove.bind(this,file)}/>
			          	<Icon type='eye-o' onClick={this.onPreview.bind(this,file)}/>
			           </span>
			        </div>
    			)
    		})}
	      <Upload {...props} fileList={fileList} >
	        <Icon type="plus"/>
	        <div className="ant-upload-text">上传图片</div>
	      </Upload>
	        <Modal visible={this.state.priviewVisible} footer={null} onCancel={this.handleCancel}>
          		<img alt="example" src={this.state.priviewImage} />
        	</Modal>
        </div>	
    )
  }
});

export default UploadFile;