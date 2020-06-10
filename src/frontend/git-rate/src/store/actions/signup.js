import * as layoutBackdropActions from './layoutBackdrop';
import { http } from '../../shared/http';
import * as layoutSnackbarActions from './layoutSnackbar';
import * as authActions from './auth';
import { push } from 'connected-react-router';

export const signup = (userName, email, password) => {
    return dispatch => {

        dispatch(layoutBackdropActions.open());
        http({
            method: "POST",
            baseURL: `${process.env.REACT_APP_AUTH_API_URL}`,
            url: "api/account/signup",
            data: {
                userName: userName,
                email: email,
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
                dispatch(layoutSnackbarActions.enqueueSnackbarError("Could not retrieve jwt and refresh token"));
            }

        }).catch(error => {
            dispatch(layoutBackdropActions.close());
            if (error && error.errors) {
                error.errors.forEach(error => dispatch(layoutSnackbarActions.enqueueSnackbarError(error.message)));
            }
            else {
                dispatch(layoutSnackbarActions.enqueueSnackbarError("And error occured during signup"));
                console.log(error);
            }
        })
    }
}