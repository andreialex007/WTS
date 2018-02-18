import React from 'react';
import styles from './../styles/common.scss';
import bootstrapCss from 'bootstrap-css-only';
import { Route, NavLink, BrowserRouter, Router, Link, withRouter } from "react-router-dom";
import Home from './home';
import Users from './users/users.jsx';
import Warehouses from './warehouses/warehouses';


class App extends React.Component {
    constructor(props) {
        super(props);

        this.routeComponents = [
            { component: Home, path: "/", exact: true, name: "Home" },
            { component: Users, path: "/users", name: "Users" },
            { component: Warehouses, path: "/warehouses", name: "Warehouses" }
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
                <nav className="navbar navbar-expand-md navbar-dark bg-dark fixed-top">
                    <div className="collapse navbar-collapse" id="navbarsExampleDefault">
                        <ul className="navbar-nav mr-auto">
                            {this.routeComponents.map((value, index) => (
                                <li key={index} className={'nav-item ' + this.isActiveRoute(value)}>
                                    <Link className="nav-link" to={value.path}>{value.name}</Link>
                                </li>
                            ))}
                        </ul>
                        <form className="form-inline my-2 my-lg-0">
                            <input className="form-control mr-sm-2" type="text" placeholder="Search" aria-label="Search"/>
                            <button className="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                        </form>
                    </div>
                </nav>
                <main role="main" className="container">
                    {this.routeComponents.map((value, index) => (
                        <Route key={index} exact={value.exact} path={value.path} component={value.component} />
                    ))}
                </main>
            </div>
        );
    }
}

export default withRouter(App);