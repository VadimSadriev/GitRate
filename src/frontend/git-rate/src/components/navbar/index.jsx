import React from 'react';
import { Link } from 'react-router-dom';
import {
    AppBar,
    Toolbar,
    Button,
    Typography
} from '@material-ui/core';
import './style.scss';


function NavBar(props) {

    return (
        <React.Fragment>
            <AppBar position="fixed" color="default">
                <Toolbar className="nav-menu">
                    <nav>
                        <Typography variant="h6" component={Link} to="/" className="nav-brand">
                            Git-Rate
                        </Typography>
                        <div className="auth-links">
                            <Button component={Link} to="/signup">SignUp</Button>
                            <Button component={Link} to="/signin">SignIn</Button>
                        </div>
                    </nav>
                </Toolbar>
            </AppBar>
        </React.Fragment>
    )
}

export default NavBar;