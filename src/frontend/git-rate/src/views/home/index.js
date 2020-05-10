import React, { useEffect } from 'react';

function Home(props) {

    useEffect(() => {
        document.title = props.title;
    })

    return (
        <React.Fragment>
            Home page
        </React.Fragment>
    )
}

export default Home;