import React from 'react';
import NavBar from '../navbar/index';
import './style.scss';

function Layout(props) {

    return (
        <React.Fragment>
            <NavBar />
            <div class="body-container">
                {props.children}
            </div>
        </React.Fragment>
    )
}

export default Layout;