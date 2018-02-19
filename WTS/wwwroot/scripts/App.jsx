import React from 'react';
import bootstrapStyles from './../styles/bootstrap-4.css';
import styles from './../styles/common.scss';
import { Route, NavLink, BrowserRouter, Router, Link, withRouter } from "react-router-dom";
import Home from './home';
import Users from './users/users.jsx';
import Warehouses from './warehouses/warehouses';
import moment from 'moment';


class App extends React.Component {
    constructor(props) {
        super(props);

        this.routeComponents = [
            { component: Home, path: "/", exact: true, name: "Home", icon: "fa-dashboard" },
            { component: Users, path: "/users", name: "Users", icon: "fa-users" },
            { component: Warehouses, path: "/warehouses", name: "Warehouses", icon: "fa-cubes" }
        ];
    }

    isActiveRoute(item) {
        let isActive = !!item.exact
            ? item.path.toLowerCase() == this.props.location.pathname.toLowerCase()
            : this.props.location.pathname.toLowerCase().startsWith(item.path.toLowerCase());
        return isActive ? "active" : "";
    }

    render() {

        return (
            <div>
                <nav className="navbar navbar-expand-lg navbar-dark bg-dark fixed-top" id="mainNav">
                    <NavLink className="navbar-brand" to="/">
                        <i className="fa fa-gamepad" aria-hidden="true"></i>&nbsp;
                        Stock tracking system
                    </NavLink>
                    <button className="navbar-toggler navbar-toggler-right" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarResponsive">
                        <ul className="navbar-nav navbar-sidenav" id="exampleAccordion">
                            {this.routeComponents.map((value, index) => (
                                <li data-toggle="tooltip" data-placement="right" title={value.name} key={index}
                                    className={'nav-item ' + this.isActiveRoute(value)}>
                                    <Link className="nav-link" to={value.path}>
                                        <i className={'fa fa-fw ' + value.icon}></i>&nbsp;
                                        <span className="nav-link-text">{value.name}</span>
                                    </Link>
                                </li>
                            ))}
                        </ul>
                        <ul className="navbar-nav sidenav-toggler">
                            <li className="nav-item">
                                <a className="nav-link text-center" id="sidenavToggler">
                                    <i className="fa fa-fw fa-angle-left"></i>
                                </a>
                            </li>
                        </ul>
                        <ul className="navbar-nav ml-auto">
                            <li className="nav-item">
                                <span className="navbar-brand user-navabar-brand" >
                                    <i className="fa fa-fw fa-address-book"></i> Admin
                                </span>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" data-toggle="modal" >
                                    <i className="fa fa-fw fa-sign-out"></i>Logout
                                </a>
                            </li>
                        </ul>
                    </div>
                </nav>
                <div className="content-wrapper">
                    <div className="container-fluid">
                        {this.routeComponents.map((value, index) => (
                            <Route key={index} exact={value.exact} path={value.path} component={value.component} />
                        ))}
                    </div>
                    <footer className="sticky-footer">
                        <div className="container">
                            <div className="text-center">
                                <small>Stock tracking system {moment().year()}</small>
                            </div>
                        </div>
                    </footer>
                    <a className="scroll-to-top rounded" href="#page-top">
                        <i className="fa fa-angle-up"></i>
                    </a>
                    <div className="modal fade" id="exampleModal" tabIndex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div className="modal-dialog" role="document">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title" id="exampleModalLabel">Ready to Leave?</h5>
                                    <button className="close" type="button" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div className="modal-body">Select "Logout" below if you are ready to end your current session.</div>
                                <div className="modal-footer">
                                    <button className="btn btn-secondary" type="button" data-dismiss="modal">Cancel</button>
                                    <a className="btn btn-primary" href="login.html">Logout</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
};


export default withRouter(App);