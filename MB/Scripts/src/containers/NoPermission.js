import React, {Component} from 'react';
import { Alert } from 'antd';

export default class NoPermission extends Component {
    render() {
        return (
            <div className="container">
              <Alert
                message="权限不够"
                description="你没有当前页面的权限，请联系你的管理员！"
                type="error"
                showIcon
                />
            </div>
        );
    }
}
