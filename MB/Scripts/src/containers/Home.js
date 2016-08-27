import React, { Component } from 'react';
import { Route, Link, State, History,IndexLink} from 'react-router';

export default class Home extends Component {
    render() {
        return (
            <div id="home" className='container' >
                <h1>Home Page</h1>
            </div>
        );
    }
}

