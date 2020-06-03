import React from 'react';
import BaseRouter from './routes';
import Layout from './components/layout';
import './style.scss';

function App() {
  return (
    <React.Fragment>
      <Layout>
        <BaseRouter />
      </Layout>
    </React.Fragment>
  );
}

export default App;
