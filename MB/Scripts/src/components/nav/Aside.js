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
    menus.forEach(group=> {
      group.items.forEach(menu=> {
        if (permission.filter(x=>x.controller == menu.controller && x.action == menu.action).length > 0) {
          menu.show = true;
        }
      });
      if (group.items.filter(x=>x.show).length > 0) {
        group.show = true;
      }
    })
  },

  render(){
    var menus = [{
      group: 'system',
      title: '系统设置',
      icon: 'setting',
      items: [
        {
          controller: 'userpermission',
          action: 'index',
          title: '权限管理'
        },
        {
          controller: 'userrole',
          action: 'index',
          title: '角色管理'
        },
        {
          controller: 'department',
          action: 'index',
          title: '部门管理'
        },
        {
          controller: 'job',
          action: 'index',
          title: '职位管理'
        }]
      },
      {
        group: 'business',
        title: '业务中心',
        icon: 'appstore',
        items: [
          {
            controller: 'category',
            action: 'index',
            title: '产品类别'
          },
          {
            controller: 'carcate',
            action: 'index',
            title: '汽车类别'
          },
          {
            controller: 'citycate',
            action: 'index',
            title: '城市管理'
          },
          {
            controller: 'storage',
            action: 'index',
            title: '仓库管理'
          }
        ]
      },
      {
        group: 'product',
        title: '产品中心',
        icon: 'hdd',
        items: [
          {
            controller: 'product',
            action: 'index',
            title: '产品列表'
          },
          {
            controller: 'product',
            action: 'create',
            title: '产品添加'
          },
          {
            controller: 'productattribute',
            action: 'index',
            title: '产品属性'
          },
          {
            controller: 'manufacturer',
            action: 'index',
            title: '品牌管理'
          }
        ]
      }
    ];
    this.setMenu(menus);
    return (
      <aside className="ant-layout-sider">
        <Menu
          mode="inline"
          openKeys={this.state.openKeys}
          selectedKeys={[this.props.current]}
          onOpen={this.onToggle}
          >
          {menus.filter(m=>m.show).map(group=> {
            return (
              <SubMenu key={group.group}
                       title={
                        <span>
                            <Icon type={group.icon} />
                            <span className="nav-text">{group.title}</span>
                        </span>
                    }>
                {group.items.filter(m=>m.show).map(m=> {
                  let linkTo= m.controller + (m.action==='index'?'':('/'+m.action));
                  return (
                    <Menu.Item key={linkTo}>
                      <Link to={linkTo}>{m.title}</Link>
                    </Menu.Item>
                  )
                })}
              </SubMenu>
            )
          })}
        </Menu>
      </aside>
    );
  }
});

export default Aside;

