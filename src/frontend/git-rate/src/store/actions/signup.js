import * as layoutBackdropActions from './layoutBackdrop';
import { http } from '../../shared/http';
import * as layoutSnackbarActions from './layoutSnackbar';

const signupSuccess = () => {
    return {
        type: "SIGNUP_SUCCESS"
    }
}

const signupFail = () => {
    return {
        type: "SIGNUP_FAIL"
    }
}

export const signup = (userName, email, password) => {
    return dispatch => {

        dispatch(layoutSnackbarActions.enqueueSnackbarSuccess("privet"));
        // dispatch(layoutBackdropActions.open());
        
        // http.post('api/signup', {
        //     userName: userName,
        //     email: email,
        //     password: password
        // }).then(resp => {
        //     console.log(resp);
        //     dispatch(layoutBackdropActions.close());
        // }).catch(error => {
        //     dispatch(layoutBackdropActions.close());
        // })
    }
}