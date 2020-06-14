import * as layoutSnackbarActions from './layoutSnackbar';
import * as layoutBackdropActions from './layoutBackdrop';
import * as authActions from './auth';
import { push } from 'connected-react-router';
import { http } from '../../shared/http';


export const signin = (userNameOrEmail, password) => {
    return dispatch => {

        if (!userNameOrEmail && !password){
            dispatch(layoutSnackbarActions.enqueueSnackbarError("Username, email or password cannot be empty"));
            return;
        }

        dispatch(layoutBackdropActions.open());
        http({
            method: "POST",
            baseURL: `${process.env.REACT_APP_AUTH_API_URL}`,
            url: "api/account/signin",
            data: {
                userNameOrEmail: userNameOrEmail,
                password: password
            }
        }).then(resp => {
            dispatch(layoutBackdropActions.close());
            const { jwt, refreshToken } = resp.data;

            if (jwt && refreshToken) {
                localStorage.setItem("token", jwt);
                localStorage.setItem("refreshToken", refreshToken);
                dispatch(authActions.authSuccess());
                dispatch(push("/"));
            }
            else {
                console.log("Could not retrieve jwt and refresh token");
                dispatch(layoutSnackbarActions.enqueueSnackbarError("And error occured during signin"));
            }

        }).catch(error => {
            dispatch(layoutBackdropActions.close());
            if (error && error.errors) {
                error.errors.forEach(error => dispatch(layoutSnackbarActions.enqueueSnackbarError(error.message)));
            }
            else {
                dispatch(layoutSnackbarActions.enqueueSnackbarError("And error occured during signin"));
                console.log(error);
            }
        })
    }
}