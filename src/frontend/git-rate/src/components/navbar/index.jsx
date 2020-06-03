import React from 'react';
import { Link } from 'react-router-dom';
import {
    AppBar,
    Toolbar,
    Button,
    Typography
} from '@material-ui/core';


function NavBar(props) {

    return (
        <React.Fragment>
            <AppBar position="fixed" color="default">
                <Toolbar>
                    <Typography variant="h6" component={Link} to="/">
                        Git-Rate
                    </Typography>
                    <div>
                        <Button>SignIn</Button>
                    </div>
                </Toolbar>
            </AppBar>
        </React.Fragment>
    )
}

export default NavBar;