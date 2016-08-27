import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux'
import { Spin } from 'antd';
import connectStatic from '../utils/connectStatic'
import * as authActions from '../actions/auth'
import { ProfileForm } from '../components';

var Profile = React.createClass({

    displayName: 'Profile',

    onSubmit(data){
        console.log(data);
    },

    render() {
        let {auth:{loading, saveProfileError,profile}} =this.props;
        return (
            <div id="profile" className='container'>
                <h1>Profile</h1>

                <div id='profile-info'>
                    <Spin spining={this.props.auth.loading}/>
                    <ProfileForm onSubmit={this.onSubmit} submitting={loading} formError={saveProfileError}
                                 initialValues={profile}
                                 ref='profileForm'/>
                </div>
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
    return {actions: bindActionCreators(authActions, dispatch)}
}

function fetchData(dispatch, getState, params, query) {
    dispatch(authActions.loadAuthToken(getState().auth.token));
    return dispatch(authActions.loadProfile());
}


export default connectStatic({fetchData})(connect(mapStateToProps, mapDispatchToProps)(Profile))

