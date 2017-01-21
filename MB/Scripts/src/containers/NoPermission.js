import React, {Component} from 'react';
import { browserHistory } from 'react-router';
import { Alert, Button } from 'antd';
import connectStatic from '../utils/connectStatic'

class NoPermission extends Component {
    render() {
        return (
            <div className="container">
              <Alert
                message="权限不够"
                description="你没有当前页面的权限，请联系你的管理员！"
                type="error"
                showIcon
                />
              <Button onClick={()=>{
                browserHistory.push('login')
              }}>重新登录</Button>

            </div>
        );
    }
}

const statics = {
  noLayout: true
};

export default connectStatic(statics)(NoPermission)

