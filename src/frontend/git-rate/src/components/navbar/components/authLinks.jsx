import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from '@material-ui/core';

function AuthLinks(props) {

    return (
        <React.Fragment>
            <div className="auth-links">
                <Button component={Link} to="/signup">SignUp</Button>
                <Button component={Link} to="/signin">SignIn</Button>
            </div>
        </React.Fragment>
    )
}

export default AuthLinks;