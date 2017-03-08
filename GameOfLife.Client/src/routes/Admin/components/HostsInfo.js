import React from 'react'
import {
  Row,
  Col
} from 'react-bootstrap'

export const HostsInfo = (props) => {
  var info = props.tasks && props.tasks.map((task, index) => {
    return (
      <Row key={index} className='tasks-row'>
        <Col xs={6}>{task.TaskId}</Col>
        <Col xs={6}>{task.PartId}</Col>
      </Row>
    )
  })
  return (
    <div className=''>
      {props.tasks.length > 0  && <Row className='tasks-header'>
        <Col xs={6}>
          <p><b>{'Task ID'}</b></p>
        </Col>
        <Col xs={6}>
          <p><b>{'Part #'}</b></p>
        </Col>
      </Row>
      }
      {info}
    </div>
  )
}

export default HostsInfo
