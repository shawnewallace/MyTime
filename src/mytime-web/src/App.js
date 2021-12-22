import React from 'react';
import Nav from './components/Nav/Nav'
import Month from './components/Month/Month'
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.js';

function App() {
  return (
    <div className="App">
      <Nav />
			<Month />
    </div>
  );
}

export default App;
