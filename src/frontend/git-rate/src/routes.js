import React from 'react';
import { Route, Switch, BrowserRouter as Router } from 'react-router-dom';
import Home from './views/home';
import NotFound from './components/not-found';

function Routers(props) {

    return (
        <Router>
            <Switch>
                <Route exact path='/' render={() => <Home title="GitRate - Home" />} />
                <Route render={() => <NotFound title="GitRate - Not Found" />} />
            </Switch>
        </Router>
    )
}

export default Routers;