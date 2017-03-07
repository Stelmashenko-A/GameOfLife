import React from 'react'
import Header from '../../components/Header'
import './CoreLayout.scss'
import '../../styles/core.scss'
import { Grid } from 'react-bootstrap'

export const CoreLayout = ({ children }) => (
  <div>
    <Grid className='core-layout__viewport'>
      <Header />
    </Grid>
    <Grid className='core-layout__viewport'>
      {children}
    </Grid>
  </div>
)

CoreLayout.propTypes = {
  children: React.PropTypes.element.isRequired
}

export default CoreLayout
