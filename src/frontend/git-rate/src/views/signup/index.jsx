import React, { useEffect } from 'react';
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

    return (
        <React.Fragment>
            <Container maxWidth='sm'>
                <Card>
                    <CardHeader className='card-header' title='Register account' />
                    <CardContent className='card-content'>
                        <TextField label='User Name' color='secondary'  />
                        <TextField label='Email'  />
                        <TextField label='Password' type='password' color='secondary' />
                    </CardContent>
                    <CardActions className='card-footer'>
                        <Button variant="contained" color="primary" >Sign Up</Button>
                    </CardActions>
                </Card>
            </Container>
        </React.Fragment>
    )
}

export default SignUp;