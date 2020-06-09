export const enqueueSnackbar = notification => {
    const key = notification.options && notification.options.key;

    return {
        type: "ENQUEUE_LAYOUTSNACKBAR",
        payload: {
            notification: {
                ...notification,
                key: key || new Date().getTime() + Math.random()
            }
        }
    }
}

export const closeSnackbar = key => {

    return {
        type: "CLOSE_LAYOUTSNACKBAR",
        payload: {
            dismissAll: !key,
            key
        }
    }
}

export const removeSnackbar = key => {

    return {
        type: "REMOVE_LAYOUTSNACKBAR",
        payload: {
            key
        }
    }
}

export const enqueueSnackbarError = message => {
    const key = new Date().getTime() + Math.random();

    return {
        type: "ENQUEUE_LAYOUTSNACKBAR",
        payload: {
            notification: {
                message: message,
                key,
                options: {
                    key,
                    variant: "error"
                }
            }
        }
    }
}

export const enqueueSnackbarSuccess = message => {

    const key = new Date().getTime() + Math.random();
    return {
        type: 'ENQUEUE_LAYOUTSNACKBAR',
        payload: {
            notification: {
                message: message,
                key,
                options: {
                    key,
                    variant: 'success'
                }
            }
        }
    }
}