import React ,{PropTypes}from 'react';
import { Breadcrumb, Icon , Button, Dropdown, Menu} from 'antd';
import { IndexLink, Link } from 'react-router';
var Header = React.createClass({

  getDefaultProps(){
    return {
      breadcrumb: [],
      token:{}
    }
  },

  render(){
    const { breadcrumb,token }=this.props;
    return (
      <div className="ant-layout-header">
        <div className="flex flex-nowrap" data-flex="dir:left box:justify">
          <div className="ant-layout-logo flex-align-center">
            <IndexLink to="/" ><img src="/content/logo.png"/></IndexLink>
          </div>
          <div className="ant-layout-breadcrumb flex-align-center">
            <Breadcrumb>
              <Breadcrumb.Item ><IndexLink to="/" >首页</IndexLink></Breadcrumb.Item>
              {breadcrumb.map((item, index)=> {
                return (
                  <Breadcrumb.Item key={index}>
                    {item.href ? (
                      <Link to={item.href}>{item.title}</Link>
                    ) : <span>{item.title}</span>}
                  </Breadcrumb.Item>
                )
              })}
            </Breadcrumb>
          </div>
          <div className="ant-layout-tools flex-align-center">
            <Button icon="reload" onClick={this.props.onRefresh}/>
            <Dropdown placement="bottomRight" overlay={<UserPanel/>} trigger={['click']}>
              <Button>
                <span className='username'>{token.userName}</span>
                <Icon className='arrow' type="down"/>
              </Button>
            </Dropdown>
          </div>
        </div>
      </div>
    );
  }
});

var UserPanel = React.createClass({

  render(){
    return (
      <Menu>
        <Menu.Item>
          <a><Icon type="user" />个人信息</a>
        </Menu.Item>
        <Menu.Item>
          <a><Icon type="logout" />退出登录</a>
        </Menu.Item>
      </Menu>
    )
  }
});

export default Header;

