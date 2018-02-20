import React from 'react';
import { Route, NavLink, BrowserRouter, Router, Link, withRouter } from "react-router-dom";
import reducers from './reducers';
import { connect } from 'react-redux';
import { login } from './actions';

class Login extends React.Component {
    constructor(props) {
        super(props);
    }

    login() {
        this.props.login({ title: 'Another login' });
    }

    render() {

        return (
            <div>
                <div className="container">
                    <div className="card card-login mx-auto mt-5">
                        <div className="card-header">{this.props.title}</div>
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


export default
    connect((state, ownProps) => ({ title: state.login.title }),
        {
            login
        })(withRouter(Login));