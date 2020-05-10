import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';

export default function configureStore(initialState) {

    const reducers = {
    };

    const middlewares = [
        thunk
    ];

    const rootReducer = combineReducers({
        ...reducers
    });

    return createStore(
        rootReducer,
        compose(applyMiddleware(...middlewares))
    );
}