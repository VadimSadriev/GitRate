import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import { Provider } from 'react-redux';
import { createBrowserHistory } from 'history';
import configureStore from './store';
import * as serviceWorker from './serviceWorker';
import { ConnectedRouter } from 'connected-react-router';

const history = createBrowserHistory({ basename: "/" });

const initialState = {};

const store = configureStore(initialState, history);

ReactDOM.render(
  <React.Fragment>
    <Provider store={store}>
      <ConnectedRouter history={history}>
        <App />
      </ConnectedRouter>
    </Provider>
  </React.Fragment>,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
