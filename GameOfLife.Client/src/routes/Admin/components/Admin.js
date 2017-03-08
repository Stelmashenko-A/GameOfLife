import React from 'react'
import {
  Row,
  Col,
  Table
} from 'react-bootstrap'
import HostsInfo from './HostsInfo'

import './Admin.scss'

export const Admin = (props) => {
  var hosts = props.hosts.map(function (host, index) {
    return (
      <tr key={index}>
        <td>{host.RouteId}</td>
        <td>{host.Host}</td>
        <td style={{ paddingLeft: '15px', paddingRight: '15px' }}><HostsInfo tasks={host.CurrentTasks} /></td>
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
              <th>{'Tasks info'}</th>
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
