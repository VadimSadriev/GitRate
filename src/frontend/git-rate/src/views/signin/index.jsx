import React, { useEffect, useState } from 'react';
import {
    Container,
    Card,
    CardHeader,
    CardContent,
    CardActions,
    TextField,
    Button
} from '@material-ui/core'
import { connect } from 'react-redux';
import * as actions from '../../store/actions/signin';

function SignIn(props) {

    useEffect(() => {
        document.title = props.title;
    })

    const [userNameOrEmail, setUserNameOrEmail] = useState("");

    const [password, setPassword] = useState("");

    const onUserNameOrEmailChanged = e => setUserNameOrEmail(e.target.value);

    const onPasswordChanged = e => setPassword(e.target.value);

    const onSignIn = e => {
        props.signin(userNameOrEmail, password);
    }

    return (
        <React.Fragment>
            <Container maxWidth='sm'>
                <Card>
                    <CardHeader className='card-header' title='Log in with user name or email' />
                    <CardContent className='card-content'>
                        <TextField label='UserName or Email' color='secondary' onChange={onUserNameOrEmailChanged}/>
                        <TextField label='Password' type='password' color='secondary' onChange={onPasswordChanged}/>
                    </CardContent>
                    <CardActions className='card-footer'>
                        <Button variant="contained" color="primary" onClick={onSignIn}>Sign In</Button>
                    </CardActions>
                </Card>
            </Container>
        </React.Fragment>
    )
}

const mapDispatchToProps = dispatch => {
    return {
        signin: (userNameOrEmail, password) => {
            dispatch(actions.signin(userNameOrEmail, password));
        }
    }
}

export default connect(null, mapDispatchToProps)(SignIn);