const initialState = {
    isOpen: false
}

const layoutBackdropReducer = (state = initialState, action) => {
    switch (action.type) {
        case "LAYOUTBACKDROP_OPEN": {
            return {
                ...state,
                isOpen: true
            }
        }
        case "LAYOUTBACKDROP_CLOSE": {
            return {
                ...state,
                isOpen: false
            }
        }
        default:
            return state;
    }
}

export default layoutBackdropReducer;