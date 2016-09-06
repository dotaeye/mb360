import React ,{PropTypes}from 'react';
import { Link } from 'react-router'
import { Menu, Breadcrumb, Icon } from 'antd';
const SubMenu = Menu.SubMenu;
const MenuItemGroup = Menu.ItemGroup;

var Aside = React.createClass({

  getInitialState() {
    return {
      openKeys: [this.props.open]
    };
  },
  onToggle(info) {
    this.setState({
      openKeys: info.open ? info.keyPath : info.keyPath.slice(1),
    });
  },

  hasMenu(controller,action){
    const { permission }=this.props;
    return permission.find(x=>x.controller==controller&&x.action==x.action);
  },

  render(){
    return (
      <aside className="ant-layout-sider">
        <Menu
          mode="inline"
          openKeys={this.state.openKeys}
          selectedKeys={[this.props.current]}
          onOpen={this.onToggle}
          >
          <SubMenu key="system"
                   title={
                        <span>
                            <Icon type="setting" />
                            <span className="nav-text">系统设置</span>
                        </span>
                    }>
              {this.hasMenu('userpermission','index')&&(
               <Menu.Item key="userpermission">
                 <Link to='userpermission'>权限管理</Link>
               </Menu.Item>)}
             
              {this.hasMenu('userrole','index')&&(
              <Menu.Item key="userrole">
                <Link to='userrole'>角色管理</Link>
              </Menu.Item>)}

                {this.hasMenu('department','index')&&(
              <Menu.Item key="department">
               <Link to='department'>部门管理</Link>
              </Menu.Item>)}
                {this.hasMenu('job','index')&&(
                            <Menu.Item key="job">
                             <Link to='job'>职位管理</Link>
                            </Menu.Item>)}
          </SubMenu>
        </Menu>
      </aside>
    );
  }
});

export default Aside;

