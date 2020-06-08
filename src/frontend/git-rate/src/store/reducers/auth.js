const initialState = {
    isLogged: false
}

const authReducer = (state = initialState, action) => {

    switch (action.type) {
        case "AUTH_SUCCESS": {
            return {
                ...state,
                isLogged: true
            }
        }
        case "AUTH_FAIL":{
            return {
                ...state,
                isLogged: false
            }
        }
        default:
            return state;
    }
}

export default authReducer;