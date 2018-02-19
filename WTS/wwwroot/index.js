import React from 'react';
import ReactDOM from 'react-dom';
import App from './scripts/app';
import Login from './scripts/login/login';
import { Route, NavLink, BrowserRouter, Router, Link, withRouter, Switch } from "react-router-dom";
import { Provider } from 'react-redux';
import { createStore } from 'redux';
import reducers from './scripts/reducers';

let store = createStore(reducers);

ReactDOM.render(
  <BrowserRouter>
    <Provider store={store}>
      <div>
        <Route exact={true} path="/" component={App} />
        <Route exact={true} path="/login" component={Login} />
      </div>
    </Provider>
  </BrowserRouter>,
  document.getElementById('app')
);

module.hot.accept();