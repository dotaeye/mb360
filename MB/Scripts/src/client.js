import 'babel-polyfill';
import './polyfills';
import React from 'react';
import ReactDOM from 'react-dom';
import { Router, browserHistory, RouterContext} from 'react-router';
import { createStore, applyMiddleware } from 'redux'
import { Provider } from 'react-redux';
import { useBasename } from 'history'
import clientMiddleware from './middleware/clientMiddleware'
import createRoutes from './routes'
import ApiClient from './utils/ApiClient';
import fetchData from './utils/fetchData';
import baseStorage from './utils/baseStorage';
import reducers from './reducers'
import configs from './configs';

import 'antd/dist/antd.css';
import 'flex.css/dist/data-flex.css'; //flex布局
import './public/styles/main.css';

baseStorage.setNamespace('mb_');

const createStoreWithMW = applyMiddleware(clientMiddleware(new ApiClient()))(createStore);
const store = createStoreWithMW(reducers);
const routes = createRoutes(store);
const history = useBasename(()=>browserHistory)({
  basename: '/admin'
});

window.React = React; // enable debugger

ReactDOM.render(
  <Provider store={store} key="provider">
    <Router routes={routes} history={history} />
  </Provider>,
  document.getElementById('main')
);
