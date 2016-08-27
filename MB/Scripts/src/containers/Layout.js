import React ,{PropTypes}from 'react';

import { Aside, Header } from '../components';

var Layout = React.createClass({

  propTypes: {
    user: PropTypes.object.isRequired,
    permissions: PropTypes.array.isRequired
  },

  render(){
    return (
     <div className='ant-layout-wrapper'>
          <Aside/>
          <Header/>
          <div className="ant-layout-container">
            <div className="ant-layout-content">{this.props.children}</div>
          </div>
      </div>
    );
  }
});

export default Layout;

