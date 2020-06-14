import React from 'react';
import { connect } from 'react-redux';
import { withSnackbar } from 'notistack';
import { Button } from '@material-ui/core';
import { Close } from '@material-ui/icons';
import { removeSnackbar } from '../../store/actions/layoutSnackbar';

const defaultOptions = {
    anchorOrigin: {
        vertical: 'top',
        horizontal: 'right'
    },
    persist: true
}

class SnackbarContainer extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            displayed: []
        }
    }

    storeDisplayed = key => {
        this.setState({
            displayed: [...this.state.displayed, key]
        })
    }

    removeDisplayed = key => {
        this.setState({
            displayed: this.state.displayed.filter(x => x !== key)
        });
    }

    componentDidUpdate() {
        const { notifications } = this.props;

        notifications.forEach(notification => {

            const { key, message, options = {}, dismissed = false } = notification;

            if (dismissed) {
                this.props.closeSnackbar(key);
                return;
            }

            // if snackback is displayed -> do nothing
            if (this.state.displayed.includes(key))
                return;

            // show snackbar with notistack
            this.props.enqueueSnackbar(message, {
                key,
                ...defaultOptions,
                ...options,
                action: key => (
                    <Button onClick={() => this.props.closeSnackbar(key)}>
                        <Close fontSize="small" />
                    </Button>
                ),
                onClose: (event, reason, key) => {
                    if (options.onClose) {
                        options.onClose(event, reason, key);
                    }
                },
                onExited: (event, key) => {
                    this.props.removeSnackbar(key);
                    this.removeDisplayed(key);
                }
            });

            this.storeDisplayed(key);
        });
    }

    render() {
        return null;
    }
}

const mapStateToProps = state => {
    return {
        notifications: state.layoutSnackbar.notifications
    }
}

const mapDispatchToProps = dispatch => {
    return {
        removeSnackbar: key => dispatch(removeSnackbar(key))
    }
}

const layoutWithSnackbar = withSnackbar(SnackbarContainer);

export default connect(mapStateToProps, mapDispatchToProps)(layoutWithSnackbar);