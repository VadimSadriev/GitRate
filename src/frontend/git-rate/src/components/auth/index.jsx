import React from 'react';
import { Route, Redirect } from 'react-router-dom';

export const AuthRoute = ({component: Component, ...props}) => {

   const { isLogged } = props;

    if (isLogged){
        return (
            <Route {...props} render={() => <Component {...props} />} />
        )
    }

    return <Redirect to={{
        pathname: '/signin',
        state: { from: props.location }
        }}
      />
}