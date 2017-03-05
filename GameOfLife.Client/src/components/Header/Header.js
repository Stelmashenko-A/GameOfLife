import React from 'react'
import {IndexLink, Link} from 'react-router'
import './Header.scss'

export const Header = () => (
  <div>
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
  </div>
)

export default Header
