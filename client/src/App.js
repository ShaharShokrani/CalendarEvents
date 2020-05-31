import React, { Component } from 'react';
import EventsTable from './EventsTable';
import logo from './logo.svg';

import './App.css';

class App extends Component {
  constructor(props) {
    super(props);
    this.state = { apiResponse: "" };
  }

  render() {
    return (
      <div className="App">
        <h1 className="App-title">Events table Test</h1>
        <EventsTable/>
      </div>
    )
  }
}

export default App;
