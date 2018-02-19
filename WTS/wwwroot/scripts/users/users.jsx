import React from 'react';
import { NavLink } from "react-router-dom";


export default class Users extends React.Component {
    constructor(props) {
        super(props);

    }

    render() {
        return (
            <div>
                <ol className="breadcrumb">
                    <li className="breadcrumb-item">
                        <NavLink to="/">Home</NavLink>
                    </li>
                    <li className="breadcrumb-item active">Users</li>
                </ol>
                <h1>Users</h1>
                <hr />
            </div>
        );
    }
}