import React from 'react';


const login = (state = {
    title: "Login"
}, action) => {
    switch (action.type) {
        case 'LOGIN_USER':
            console.log("try login");
            return {
                ...state,
                title: action.payload.title
            };
        default:
            return state;
    }
}

export { login };