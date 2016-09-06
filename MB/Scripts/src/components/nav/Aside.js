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

  setMenu(menus){
    const { permission }=this.props;
    menus.forEach(menu=>{
      if(permission.filter(x=>x.controller==menu.controller && x.action==menu.action).length>0){
        menu.show = true;
      }
    })
  },

  render(){
    var systemMenus=[{
      controller:'userpermission',
      action:'index',
      title:'权限管理'
    },
    {
      controller:'userrole',
      action:'index',
      title:'角色管理'
    },
    {
      controller:'department',
      action:'index',
      title:'部门管理'
    },
    {
      controller:'job',
      action:'index',
      title:'职位管理'
    }];

    this.setMenu(systemMenus);
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
              {systemMenus.filter(m=>m.show).map(m=>{
                return (
                  <Menu.Item key={m.controller}>
                    <Link to={m.controller}>{m.title}</Link>
                  </Menu.Item>
                  )
              })}
          </SubMenu>
        </Menu>
      </aside>
    );
  }
});

export default Aside;

