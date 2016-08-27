import React, {Component} from 'react';
import { Alert } from 'antd';

export default class NotFound extends Component {
    render() {
        return (
            <div className="container">
              <Alert
                message="页面不存在"
                description="当前页面不存在！"
                type="error"
                showIcon
                />
            </div>
        );
    }
}
