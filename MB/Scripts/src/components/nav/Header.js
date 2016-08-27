import React ,{PropTypes}from 'react';
import { Breadcrumb, Icon , Button, Dropdown} from 'antd';
import { IndexLink, Link } from 'react-router';
var Header = React.createClass({

  propTypes: {
    //onRefresh: PropTypes.fuc
  },
  getDefaultProps(){
    return {
      breadcrumb: []
    }
  },

  render(){
    const { breadcrumb }=this.props;
    return (
      <div className="ant-layout-header">
        <div className="flex flex-nowrap" data-flex="dir:left box:justify">
          <div className="ant-layout-logo flex-align-center">
            <IndexLink to="/" ><h1>Logo</h1></IndexLink>
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
            <Dropdown placement="bottomRight" overlay={<UserPannel/>} trigger={['click']}>
              <Button>
                <span className='avatar'></span>
                <span className='username'>用户名</span>
                <Icon className='arrow' type="down"/>
              </Button>
            </Dropdown>
          </div>
        </div>
      </div>
    );
  }
});

var UserPannel = React.createClass({

  render(){
    return (
      <div>UserPannel</div>
    )
  }
});

export default Header;

