
export const Login = (username, password, rememberMe) => {
    return {
        type: "LOGIN_USER",
        payload: {
            username,
            password,
            rememberMe
        }
    };
};