import React from 'react'
import './HomeView.scss'
import { IndexLink, Link } from 'react-router'

export const HomeView = () => (
  <div className='home'>
    <Link to='/dashboard' className='welcome-message'>Game <span className='or'>or</span><span className='of'> of</span> life</Link>
  </div>
)

export default HomeView
