import React, { Component } from 'react';

import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';


class EventsTable extends Component {
  constructor(props) {
    super(props);
    this.state = { eventsData: [] };
  }

  srotByDate(eventA, eventB) {
    var keyA = new Date(eventA.start),
      keyB = new Date(eventB.start);
    // Compare the 2 dates
    if (keyA < keyB) return -1;
    if (keyA > keyB) return 1;
    return 0;
  }
  callAPI() {
    // fetch("http://localhost:61318/api/events")
    fetch("http://localhost:61318/api/events")
      .then(res => res.text())
      .then(res => {
        let eventsData = JSON.parse(res);
        if (!eventsData)
          return;
        let uniqueData = Array.from(new Set(eventsData.map(e => e.base64Id)))
          .map(base64Id => {
            return eventsData.find(e => e.base64Id === base64Id)
          });
        this.setState({
          eventsData: uniqueData.sort(this.srotByDate)
        });
      })
      .catch(err => console.log(err));
  }
  componentDidMount() {
    this.callAPI();
  }

  render() {


    return (
      <Paper>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="right">Buy</TableCell>
              <TableCell align="right">Date</TableCell>
              <TableCell align="right">Name</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {this.state.eventsData.map(row => (
              <TableRow key={row.base64Id}>
                <TableCell align="right"> <a href={row.url} target="_blank">Buy Now</a> </TableCell>
                <TableCell align="right">{row.start}</TableCell>
                <TableCell component="th" scope="row" align="right">{row.name}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Paper>
    );
  }
}

export default EventsTable