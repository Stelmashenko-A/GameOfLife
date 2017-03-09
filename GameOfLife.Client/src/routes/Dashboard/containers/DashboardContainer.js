import { connect } from 'react-redux'

import Register from '../components/Dashboard'

import { actions } from '../modules/dashboard'

const mapDispatchToProps = {
  onPartsChange: actions.onPartsChangeHandler,
  onStepsChange: actions.onStepsChangeHandler,
  onSizeChange: actions.onSizeChangeHandler,
  onFieldChange: actions.onFieldChangeHandler,
  nextButtonHandler: actions.nextButtonHandler,
  nextButtonMaxHandler: actions.nextButtonMaxHandler,
  prevButtonMaxHandler: actions.prevButtonMaxHandler,
  prevButtonHandler: actions.prevButtonHandler,
  calculateHandler: actions.calculateHandler,
  resetButtonHandler: actions.resetButtonHandler,
  hideErrorHandler: actions.hideErrorHandler
}

const mapStateToProps = (state) => ({
  currentStep: state.dashboard.currentStep,
  steps: state.dashboard.steps,
  parts: state.dashboard.parts,
  fieldSize: state.dashboard.fieldSize,
  field: state.dashboard.field,
  loading: state.dashboard.loading,
  loaded: state.dashboard.loaded,
  host: state.dashboard.host,
  partsLoaded: state.dashboard.partsLoaded,
  error: state.dashboard.error,
  animation: state.dashboard.animation
})

export default connect(mapStateToProps, mapDispatchToProps)(Register)
