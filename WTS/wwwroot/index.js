import React from 'react';
import ReactDOM from 'react-dom';
import App from './scripts/app';
import Login from './scripts/login/login';
import { Route, NavLink, BrowserRouter, Router, Link, withRouter, Switch } from "react-router-dom";

ReactDOM.render(
  <BrowserRouter>
    <div>
      <Route exact={true} path="/" component={App} />
      <Route exact={true} path="/login" component={Login} />
    </div>
  </BrowserRouter>,
  document.getElementById('app')
);

module.hot.accept();