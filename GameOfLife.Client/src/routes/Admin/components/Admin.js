import React from 'react'
import {
  Row,
  Col,
  FormGroup,
  FormControl,
  ButtonToolbar,
  ButtonGroup,
  Button,
  Table
} from 'react-bootstrap'

import './Admin.scss'

export const Admin = (props) => {
  var hosts = props.hosts.map(function (host, index) {
    return (
      <tr key={index}>
        <td>{host.RouteId}</td>
        <td>{host.Host}</td>
        <td>{'Task ID'}</td>
        <td>{'Part #'}</td>
        <td>{host.LastConnection}</td>
      </tr>
    )
  })

  return (
    <Row>
      <Col xs={12}>
        <Table striped bordered condensed hover>
          <thead>
            <tr>
              <th>{'Route ID'}</th>
              <th>{'Host'}</th>
              <th>{'Task ID'}</th>
              <th>{'Part #'}</th>
              <th>{'Last Connection'}</th>
            </tr>
          </thead>
          <tbody>
            {hosts}
          </tbody>
        </Table>
      </Col>
    </Row>
  )
}

export default Admin
