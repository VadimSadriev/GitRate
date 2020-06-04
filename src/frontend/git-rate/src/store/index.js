import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { connectRouter, routerMiddleware } from 'connected-react-router'
import layoutBackdropReducer from './reducers/layoutBackdrop';

export default function configureStore(initialState, history) {

    const reducers = {
        layoutBackdrop: layoutBackdropReducer
    };

    const middlewares = [
        thunk,
        routerMiddleware(history)
    ];

    const rootReducer = combineReducers({
        ...reducers,
        router: connectRouter(history)
    });

    return createStore(
        rootReducer,
        compose(applyMiddleware(...middlewares))
    );
}