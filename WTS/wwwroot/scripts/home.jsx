import React from 'react';


export default class Home extends React.Component {
    constructor(props) {
        super(props);

    }

    render() {
        return (
            <div>
                <ol className="breadcrumb">
                    <li className="breadcrumb-item active">Home</li>
                </ol>
                <h1>Home</h1>
                <hr />
                <span>This is a home page</span>
            </div>
        );
    }
}