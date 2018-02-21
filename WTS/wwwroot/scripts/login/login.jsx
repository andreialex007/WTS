import React from 'react';
import { Route, NavLink, BrowserRouter, Router, Link, withRouter } from "react-router-dom";
import reducers from './reducers';
import { connect } from 'react-redux';
import { login } from './actions';

class Login extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            username: "",
            password: "",
            rememberMe: false
        };
    }

    login() {
        //this.props.login({ title: 'Another login' });
        console.log(this.state);
    }

    onChange(event) {
        this.setState({ ...this.state, [event.target.name]: event.target.value });
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
                                        type="text"
                                        name="username"
                                        value={this.state.username}
                                        onChange={(event) => this.onChange(event)}
                                        aria-describedby="emailHelp"
                                        placeholder="Enter email" />
                                </div>
                                <div className="form-group">
                                    <label >Password</label>
                                    <input className="form-control"
                                        type="password"
                                        name="password"
                                        value={this.state.password}
                                        onChange={(event) => this.onChange(event)}
                                        placeholder="Password" />
                                </div>
                                <div className="form-group">
                                    <div className="form-check">
                                        <label className="form-check-label">
                                            <input className="form-check-input"
                                                checked={this.state.rememberMe}
                                                name="rememberMe"
                                                value="true"
                                                onChange={(event) => this.onChange(event)}
                                                type="checkbox" /> Remember me
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
    connect((state, ownProps) => ({
        username: state.login.username,
        password: state.login.password,
        rememberMe: state.login.rememberMe
    }),
        {
            login
        })(withRouter(Login));