import {
    applyMiddleware,
    combineReducers,
    createStore,
    } from 'redux';
import { login } from './login/reducers';

const todoApp = combineReducers({
    login
});

export default todoApp