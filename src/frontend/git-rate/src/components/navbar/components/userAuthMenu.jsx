import React, { useState } from 'react';
import {
    Typography,
    Button,
    Collapse,
    List,
    ListItem,
    ListItemText
} from '@material-ui/core';
import { ExpandLess, ExpandMore } from '@material-ui/icons';

function UserAuthMenu(props) {

    const [isOpen, setIsOpen] = useState(false);

    return (
        <React.Fragment>
            <Typography component={"span"} className="user-auth-menu-root">
                <Button onClick={() => setIsOpen(!isOpen)}>
                    {props.user.userName} {isOpen ? <ExpandLess /> : <ExpandMore />}
                </Button>
                <Collapse className="user-auth-menu" in={isOpen} timeout="auto" unmountOnExit>
                    <List component="div" disablePadding>
                        <ListItem button className="user-auth-menu-item">
                            <ListItemText primary="SignOut" />
                        </ListItem>
                    </List>
                </Collapse>
            </Typography>

        </React.Fragment>
    )
}

export default UserAuthMenu;