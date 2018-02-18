import React from 'react';
import ReactDOM from 'react-dom';
import App from './scripts/app';
import { Route, NavLink, BrowserRouter, Router, Link, withRouter, Switch } from "react-router-dom";

ReactDOM.render(
  <BrowserRouter>
    <App />
  </BrowserRouter>,
  document.getElementById('app')
);

module.hot.accept();