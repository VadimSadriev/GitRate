const initialState = {
    notifications: []
}

const layoutSnackbarReducer = (state = initialState, action) => {

    switch (action.type) {
        case "ENQUEUE_LAYOUTSNACKBAR": {
            return {
                ...state,
                notifications: [
                    ...state.notifications,
                    {
                        ...action.payload.notification
                    }
                ]
            }
        }
        case "CLOSE_LAYOUTSNACKBAR": {
            return {
                ...state,
                notifications: state.notifications.map(notification => (
                    (action.dismissAll || notification.key === action.payload.key)
                        ? { ...notification, dismissed: true }
                        : { ...notification }
                )),
            }
        }
        case "REMOVE_LAYOUTSNACKBAR": {
            return {
                ...state,
                notifications: state.notifications.filter(
                    notification => notification.key !== action.payload.key,
                )
            }
        }
        default:
            return state;
    }
}

export default layoutSnackbarReducer;