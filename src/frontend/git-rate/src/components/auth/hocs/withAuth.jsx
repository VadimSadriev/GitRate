import React from 'react';
import { Redirect } from 'react-router-dom';
import { connect } from 'react-redux'

export default function WithAuth(Component, rest) {

    function withAuthInternal(props){

       const { isLogged } = props.auth;

       if (isLogged){
           return (<Component {...props} {...rest} />);
       }

       return <Redirect to={{
           pathname: '/signin',
           state: { from: props.location }
           }}
         />
   }

   function mapStateToProps(state) {
       return { auth: state.auth };
   }

   return connect(mapStateToProps)(withAuthInternal);
}