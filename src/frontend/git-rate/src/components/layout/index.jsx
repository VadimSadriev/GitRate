import React from 'react';
import './style.scss';

function Layout(props) {

    return (
        <React.Fragment>
            <div class="body-container">
                {props.children}
            </div>
        </React.Fragment>
    )
}

export default Layout;