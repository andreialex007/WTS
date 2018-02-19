import { combineReducers } from 'redux'
import login from './login/reducers';

const todoApp = combineReducers({
    login
});

export default todoApp