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
            <MenuItemGroup title="权限架构">
              <Menu.Item key="userpermission">
                <Link to='userpermission'>权限管理</Link>
              </Menu.Item>
              <Menu.Item key="userrole">
                <Link to='userrole'>角色管理</Link>
              </Menu.Item>
            </MenuItemGroup>
            <MenuItemGroup title="组织架构">
              <Menu.Item key="3">部门管理</Menu.Item>
              <Menu.Item key="4">职位管理</Menu.Item>
            </MenuItemGroup>
          </SubMenu>
          <SubMenu key="sub2"
                   title={
                        <span>
                            <Icon type="appstore" />
                            <span className="nav-text"
                                title="很长很长很长的就不要放到下面了"
                                >导航二
                            </span>
                        </span>
                    }>
            <Menu.Item key="5">选项5</Menu.Item>
            <Menu.Item key="6">选项6</Menu.Item>
            <SubMenu key="sub3" title="三级导航">
              <Menu.Item key="7">选项7</Menu.Item>
              <Menu.Item key="8">选项8</Menu.Item>
            </SubMenu>
          </SubMenu>
          <SubMenu key="sub4"
                   title={
                        <span>
                            <Icon type="setting" />
                            <span className="nav-text"
                                title="管理菜单要简短"
                                >导航三
                            </span>
                        </span>
                    }>
            <Menu.Item key="9">选项9</Menu.Item>
            <Menu.Item key="10">选项10</Menu.Item>
            <Menu.Item key="11">选项11</Menu.Item>
            <Menu.Item key="12">选项12</Menu.Item>
          </SubMenu>
          <SubMenu key="sub5"
                   title={
                        <span>
                            <Icon type="camera" />
                            <span className="nav-text"
                                title="管理菜单要简短"
                                >导航四
                            </span>
                        </span>
                    }>
            <Menu.Item key="9">选项9</Menu.Item>
            <Menu.Item key="10">选项10</Menu.Item>
            <Menu.Item key="11">选项11</Menu.Item>
            <Menu.Item key="12">选项12</Menu.Item>
          </SubMenu>
          <SubMenu key="sub6"
                   title={
                        <span>
                            <Icon type="notification" />
                            <span className="nav-text"
                                title="管理菜单要简短"
                                >导航五
                            </span>
                        </span>
                    }>
            <Menu.Item key="9">选项9</Menu.Item>
            <Menu.Item key="10">选项10</Menu.Item>
            <Menu.Item key="11">选项11</Menu.Item>
            <Menu.Item key="12">选项12</Menu.Item>
          </SubMenu>
        </Menu>
      </aside>
    );
  }
});

export default Aside;

