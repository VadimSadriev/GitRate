import React, { useEffect, useState } from 'react';
import * as actions from '../../store/actions/signup';
import { connect } from 'react-redux';
import {
    Container,
    Card,
    CardHeader,
    CardContent,
    CardActions,
    Button,
    TextField
} from '@material-ui/core';
import './style.scss';

function SignUp(props) {

    useEffect(() => {
        document.title = props.title;
    })

    const [userName, setUserName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const onUserNameChanged = e => setUserName(e.target.value);
    const onEmailChanged = e => setEmail(e.target.value);
    const onPasswordChanged = e => setPassword(e.target.value);

    const onSignUp = e => {
        props.signup(userName, email, password);
    }

    return (
        <React.Fragment>
            <Container maxWidth='sm'>
                <Card>
                    <CardHeader className='card-header' title='Register account' />
                    <CardContent className='card-content'>
                        <TextField label='User Name' color='secondary' onChange={onUserNameChanged} />
                        <TextField label='Email' onChange={onEmailChanged} />
                        <TextField label='Password' type='password' color='secondary' onChange={onPasswordChanged} />
                    </CardContent>
                    <CardActions className='card-footer'>
                        <Button variant="contained" color="primary" onClick={onSignUp}>Sign Up</Button>
                    </CardActions>
                </Card>
            </Container>
        </React.Fragment>
    )
}

const mapDispatchToProps = dispatch => {
    return {
        signup: (userName, email, password) => {
            dispatch(actions.signup(userName, email, password));
        }
    }
}

export default connect(null, mapDispatchToProps)(SignUp);