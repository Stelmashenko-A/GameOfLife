import React from 'react'
import Header from '../../components/Header'
import './CoreLayout.scss'
import '../../styles/core.scss'
import {
  Grid,
  Row,
  Col,
  ControlLabel,
  Radio,
  Button
} from 'react-bootstrap'

export const CoreLayout = ({children}) => (
  <Grid>
    <Header/>
    <div className='core-layout__viewport'>
      <Row>
        <Col xs={12}>
          {children}
        </Col>
      </Row>
    </div>
  </Grid>
)

CoreLayout.propTypes = {
  children: React.PropTypes.element.isRequired
}

export default CoreLayout
