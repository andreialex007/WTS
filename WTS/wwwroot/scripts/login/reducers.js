import React from 'react';


const todos = (state = [], action) => {
    switch (action.type) {
    case 'ADD_TODO':
        return [
            ...state
        ];
    default:
        return state;
    }
}

export default todos