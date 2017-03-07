import React from 'react'
import { IndexLink, Link } from 'react-router'
import './Header.scss'
import { Row, Col } from 'react-bootstrap'

export const Header = () => (
  <Row>
    <Col xs={12}>
      <IndexLink to='/' activeClassName='route--active'>
        Home
      </IndexLink>
      {' · '}
      <IndexLink to='/dashboard' activeClassName='route--active'>
        Dashboard
      </IndexLink>
      {' · '}
      <Link to='/admin' activeClassName='route--active'>
        Admin
      </Link>
    </Col>
  </Row>
)

export default Header
