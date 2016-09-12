import React,{PropTypes} from 'react';
import { Upload , Icon} from 'antd';
import _ from 'lodash';

const UploadFile = React.createClass({
 

 	propTypes: {
        multiple: PropTypes.bool,
        defaultUrls: PropTypes.array
    },

    getDefaultProps(){

    	return {
    		multiple : false,
    		defaultUrls: []
    	}
    },


 	getInitialState() {
 		let fileList=[];
 		const { defaultUrls }=this.props;
 		if(defaultUrls.length>0){
 			fileList=defaultUrls.map((url,index)=>{
 				return {
 					id:-index,
 					status:'done',
 					url,
 				}
 			})
 		}
	    return {
	      fileList
	    };
  	},

	onUploadChange(info){

		let fileList = info.fileList;

	    // 1. 上传列表数量的限制
	    //    只显示最近上传的一个，旧的会被新的顶掉
	    fileList = fileList.slice(-1);

	    // 2. 读取远程路径并显示链接
	    fileList = fileList.map((file) => {
	      if (file.response) {
	        // 组件会将 file.url 作为链接进行展示
	        file.url = file.response.imageUrl_120;
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

	    this.setState({ fileList });
	},

	render(){
		const {multiple}=this.props;
		const props={
			action: '/api/Upload',
		    listType: 'picture-card',
		    multiple: multiple,
		    onChange:this.onUploadChange
		}

		return (
			 <Upload {...props} fileList={this.state.fileList}>
          		<Icon type="plus" />
          		<div className="ant-upload-text">上传图片</div>
        	</Upload>	
		)
	}

})

export default UploadFile;