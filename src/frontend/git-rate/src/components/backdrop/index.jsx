import React from 'react';
import { Backdrop, CircularProgress } from '@material-ui/core';
import { connect } from 'react-redux';
import './style.scss';

function LayoutBackdrop(props) {

    return (
        <React.Fragment>
            <Backdrop className="backdrop" open={this.props.isOpen}>
                <CircularProgress color="inherit" />
            </Backdrop>
        </React.Fragment>
    )
}

const mapStateToProps = state => {
    return {
        isOpen: state.layoutBackdrop.isOpen
    }
}

export default connect(mapStateToProps)(LayoutBackdrop);