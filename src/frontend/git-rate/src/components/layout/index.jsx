import React from 'react';
import NavBar from '../navbar/index';
import { withStyles } from '@material-ui/core/styles';
import LayoutBackdrop from '../backdrop';
import LayoutSnackbar from '../snackbar';
import './style.scss';

const styles = theme => ({
    toolbar: theme.mixins.toolbar
});

function Layout(props) {

    return (
        <React.Fragment>
            <NavBar />
            <div className={props.classes.toolbar}></div>
            <LayoutBackdrop />
            <LayoutSnackbar />
            <div className="body-container">
                {props.children}
            </div>
        </React.Fragment>
    )
}

export default withStyles(styles)(Layout);