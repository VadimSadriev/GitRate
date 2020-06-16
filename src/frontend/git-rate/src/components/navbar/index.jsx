import React from 'react';
import { Link } from 'react-router-dom';
import {
    AppBar,
    Toolbar,
    Button,
    Typography
} from '@material-ui/core';
import AuthLinks from './components/authLinks';
import UserAuthMenu from './components/userAuthMenu';
import { connect } from 'react-redux';
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
                        {
                            props.auth.isLogged
                                ? <UserAuthMenu user={props.auth.user} />
                                : <AuthLinks />
                        }
                    </nav>
                </Toolbar>
            </AppBar>
        </React.Fragment>
    )
}

const mapStateToProps = state => {
    return {
        auth: state.auth
    }
}

export default connect(mapStateToProps)(NavBar);