import React from 'react';
import { Route, NavLink, BrowserRouter, Router, Link, withRouter } from "react-router-dom";

class Login extends React.Component {
    constructor(props) {
        super(props);
    }

    login() {
        console.log("login");
    }

    render() {

        return (
            <div>
                <div className="container">
                    <div className="card card-login mx-auto mt-5">
                        <div className="card-header">Login</div>
                        <div className="card-body">
                            <form>
                                <div className="form-group">
                                    <label >Email address</label>
                                    <input className="form-control"
                                        id="exampleInputEmail1"
                                        type="email"
                                        aria-describedby="emailHelp"
                                        placeholder="Enter email" />
                                </div>
                                <div className="form-group">
                                    <label >Password</label>
                                    <input className="form-control"
                                        id="exampleInputPassword1"
                                        type="password"
                                        placeholder="Password" />
                                </div>
                                <div className="form-group">
                                    <div className="form-check">
                                        <label className="form-check-label">
                                            <input className="form-check-input" type="checkbox" /> Remember me
                                        </label>
                                    </div>
                                </div>
                                <a className="btn btn-primary btn-block" onClick={() => this.login()} href="javascript:;">Login</a>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
};


export default withRouter(Login);