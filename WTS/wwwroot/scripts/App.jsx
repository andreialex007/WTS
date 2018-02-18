import React from 'react';
import styles from './../styles/common.scss';
import bootstrapCss from 'bootstrap-css-only';
import { Route, NavLink, BrowserRouter } from "react-router-dom";
import Home from './home';
import Users from './users/users.jsx';
// import Warehouses from './werehouses/werehouses.jsx';


class App extends React.Component {
    constructor(props) {
        super(props);

    }

    render() {

        debugger;

        return (
            <BrowserRouter>
                <div>
                    <nav className="navbar navbar-expand-md navbar-dark bg-dark fixed-top">
                        
                        <div className="collapse navbar-collapse" id="navbarsExampleDefault">
                            <ul className="navbar-nav mr-auto">
                                <li className="nav-item active">
                                    <a className="nav-link" href="/">Home </a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href="/users">Users</a>
                                </li>
                            </ul>
                            <form className="form-inline my-2 my-lg-0">
                                <input className="form-control mr-sm-2" type="text" placeholder="Search" aria-label="Search" />
                                <button className="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                            </form>
                        </div>
                    </nav>
                    <main role="main" className="container">
                        <Route exact path="/" component={Home} />
                        <Route path="/users" component={Users} />

                    </main>
                </div>
            </BrowserRouter>
        );
    }
}

export default App;