import { connect } from 'react-redux'

import Register from '../components/Admin'

import { actions } from '../modules/admin'

const mapDispatchToProps = {
  // onPartsChange: actions.onPartsChangeHandler
}

const mapStateToProps = (state) => ({
  hosts: state.admin.hosts
})

export default connect(mapStateToProps, mapDispatchToProps)(Register)
