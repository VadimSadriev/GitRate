import React from 'react';
import { Route, Switch } from 'react-router-dom';
import Home from './views/home';
import SignUp from './views/signup';
import NotFound from './components/not-found';


function Routers(props) {

    return (
        <Switch>
            <Route exact path="/" render={() => <Home title="GitRate - Home" />} />
            <Route path="/signup" render={() => <SignUp title="GitRate - SignUp" />} />
            <Route render={() => <NotFound title="GitRate - Not Found" />} />
        </Switch>
    )
}

export default Routers;