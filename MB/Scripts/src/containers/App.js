import React, { Component, PropTypes } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link, browserHistory, Router } from 'react-router'
import { IndexLink } from 'react-router';
import configs from '../configs';
import { Aside, Header } from '../components';
import * as authActions from '../actions/auth'

var App = React.createClass({

  propTypes: {
    user: PropTypes.object
  },

  onRefresh(path){
    window.location.href = path;
  },

  render(){
    const { auth: {permission}}=this.props;
    const child = React.cloneElement(this.props.children, {key: new Date().getTime()});
    const { path, noLayout, breadcrumb, menuGroup } = child.type;
    return (
      <div id='app'>
        {!noLayout ? (
          <div className='ant-layout'>
            <Aside current={path} open={menuGroup} permission={permission}/>
            <Header onRefresh={this.onRefresh.bind(null,path)} breadcrumb={breadcrumb}/>

            <div className="ant-layout-container">
              <div className="ant-layout-content">{this.props.children}</div>
            </div>
          </div>
        ) : this.props.children}
      </div>
    );
  }
});


function mapStateToProps(state) {
  return {
    auth: state.auth
  }
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(authActions, dispatch)
  }
}


export default connect(mapStateToProps, mapDispatchToProps)(App)