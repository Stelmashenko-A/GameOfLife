import axios from 'axios'
import _ from 'lodash'

// ------------------------------------
// Constants
// ------------------------------------
export const STEPS_CHANGE = 'STEPS_CHANGE'
export const PARTS_CHANGE = 'PARTS_CHANGE'
export const SIZE_CHANGE = 'WIDTH_CHANGE'
export const FIELD_INPUT_CHANGE = 'FIELD_INPUT_CHANGE'
export const SET_LOADING = 'SET_LOADING'
export const SET_LOADED = 'SET_LOADED'
export const FIELD_CHANGE = 'FIELD_CHANGE'
export const CURRENT_STEP_CHANGE = 'CURRENT_STEP_CHANGE'
export const STEPS_PATH_CHANGE = 'STEPS_PATH_CHANGE'
export const TASK_ID_CHANGE = 'TASK_ID_CHANGE'
export const PARTS_LOADED_CHANGE = 'PARTS_LOADED_CHANGE'
export const HOST_CHANGE = 'HOST_CHANGE'
export const REMOVE_TASK = 'REMOVE_TASK'

// ------------------------------------
// Actions
// ------------------------------------
export const setHostHandler = (host) => {
  return {
    type: HOST_CHANGE,
    payload: host
  }
}

export const setPartsLoadedHandler = (partsLoaded) => {
  return {
    type: PARTS_LOADED_CHANGE,
    payload: partsLoaded
  }
}

export const setTaskIdHandler = (taskId) => {
  return {
    type: TASK_ID_CHANGE,
    payload: taskId
  }
}

export const setStepsPathHandler = (stepsPath) => {
  return {
    type: STEPS_PATH_CHANGE,
    payload: stepsPath
  }
}

export const setCurrentStepHandler = (currentStep) => {
  return {
    type: CURRENT_STEP_CHANGE,
    payload: currentStep
  }
}

export const setFieldHandler = (field) => {
  return {
    type: FIELD_CHANGE,
    payload: field
  }
}

export const setLoadingHandler = (loading) => {
  return {
    type: SET_LOADING,
    payload: loading
  }
}

export const setLoadedHandler = (loaded) => {
  return {
    type: SET_LOADED,
    payload: loaded
  }
}

export const onStepsChangeHandler = (e) => {
  var value = _.isNil(e.target.value) ? 0 : _.parseInt(e.target.value)
  return (dispatch, getState) => {
    var data = getState().dashboard
    if (data.loaded) {
      resetProggress(dispatch)
    }
    dispatch({
      type: STEPS_CHANGE,
      payload: value
    })
  }
}

export const onPartsChangeHandler = (e) => {
  var value = _.isNil(e.target.value) ? 0 : _.parseInt(e.target.value)
  return (dispatch, getState) => {
    var data = getState().dashboard
    if (data.loaded) {
      resetProggress(dispatch)
    }
    dispatch({
      type: PARTS_CHANGE,
      payload: value
    })
  }
}

export const onSizeChangeHandler = (e) => {
  var value = _.isNil(e.target.value) ? 0 : _.parseInt(e.target.value)
  return (dispatch, getState) => {
    var data = getState().dashboard
    if (data.loaded) {
      resetProggress(dispatch)
    }
    dispatch({
      type: SIZE_CHANGE,
      payload: value
    })
  }
}

export const onFieldChangeHandler = (e) => {
  return (dispatch, getState) => {
    var data = getState().dashboard
    if (data.loaded) {
      resetProggress(dispatch)
    }
    dispatch({
      type: FIELD_INPUT_CHANGE,
      payload: !(('' + e.target.className + ' ').replace(/[\n\t]/g, ' ').indexOf('active') > -1),
      x: e.target.getAttribute('x'),
      y: e.target.getAttribute('y')
    })
  }
}

// ------------------------------------
// Functions
// ------------------------------------
export const nextButtonHandler = (e) => {
  e.preventDefault()
  return (dispatch, getState) => {
    var state = getState().dashboard

    if (state.currentStep === state.stepsPath.length) {
      return
    }

    dispatch(setCurrentStepHandler(state.currentStep + 1))

    state = getState().dashboard

    var matrix = generateMatrix(state.fieldSize, state.fieldSize, state.stepsPath, state.steps, state.currentStep)

    dispatch(setFieldHandler(matrix))
  }
}

export const nextButtonMaxHandler = (e) => {
  e.preventDefault()
  return (dispatch, getState) => {
    var state = getState().dashboard

    if (state.currentStep === state.stepsPath.length) {
      return
    }

    dispatch(setLoadingHandler(true))

    for (var i = 0; i < state.steps - state.currentStep; i++) {
      setTimeout(() => {
        dispatch(setCurrentStepHandler(state.currentStep + 1))

        state = getState().dashboard

        var matrix = generateMatrix(state.fieldSize, state.fieldSize, state.stepsPath, state.steps, state.currentStep)

        dispatch(setFieldHandler(matrix))

        if (state.currentStep === state.steps) {
          dispatch(setLoadingHandler(false))
        }
      }, 500 * i)
    }
  }
}

export const prevButtonHandler = (e) => {
  e.preventDefault()
  return (dispatch, getState) => {
    var state = getState().dashboard

    if (state.currentStep === 0) {
      return
    }

    dispatch(setCurrentStepHandler(state.currentStep - 1))

    state = getState().dashboard
    var matrix = generateMatrix(state.fieldSize, state.fieldSize, state.stepsPath, state.steps, state.currentStep)

    dispatch(setFieldHandler(matrix))
  }
}

export const prevButtonMaxHandler = (e) => {
  e.preventDefault()
  return (dispatch, getState) => {
    var state = getState().dashboard

    if (state.currentStep === 0) {
      return
    }

    dispatch(setLoadingHandler(true))

    for (var i = 0; i < state.currentStep; i++) {
      setTimeout(() => {
        dispatch(setCurrentStepHandler(state.currentStep - 1))

        state = getState().dashboard

        var matrix = generateMatrix(state.fieldSize, state.fieldSize, state.stepsPath, state.steps, state.currentStep)

        dispatch(setFieldHandler(matrix))

        if (state.currentStep === 0) {
          dispatch(setLoadingHandler(false))
        }
      }, 500 * i)
    }
  }
}

export const calculateHandler = (e) => {
  e.preventDefault()
  return (dispatch, getState) => {
    dispatch(setLoadingHandler(true))
    var data = getState().dashboard

    if (data.loaded) {
      resetProggress(dispatch)
    }
    data = getState().dashboard
    var path = _.clone(data.stepsPath)
    path.push(matrixToString(data.field))

    dispatch(setStepsPathHandler(path))

    return axios({
      method: 'Post',
      url: '/values/process',
      data: {
        Id: null,
        Field: matrixToString(data.field),
        Steps: data.steps,
        Parts: data.parts
      }
    })
      .then(function (response) {
        if (response.data != null) {
          dispatch(setLoadingHandler(false))
          dispatch(setTaskIdHandler(response.data.TaskId))
          dispatch(setHostHandler(response.data.Host))

          data = getState().dashboard

          getStepsPath(data.taskId, data.partsLoaded, dispatch, getState)
        }
      }).catch(function (error) {
        if (error) {
          console.log(error)
        }
        dispatch(setLoadingHandler(false))
      })
  }
}

export const resetButtonHandler = (e) => {
  e.preventDefault()
  return (dispatch, getState) => {
    var data = getState().dashboard
    if (data.loaded) {
      resetProggress(dispatch)
    }
  }
}

export const removeTaskHandler = () => {
  return (dispatch, getState) => {
    var state = getState().dashboard
    if (!_.isNil(state.taskId)) {
      dispatch({
        type: REMOVE_TASK
      })
      axios({
        method: 'Get',
        url: '/values/remove/' + state.taskId
      }).then(function (response) {
        dispatch(setTaskIdHandler(null))
      }).catch(function (error) {
        console.log(error)
      })
    }
  }
}

function resetProggress (dispatch) {
  dispatch(setStepsPathHandler([]))
  dispatch(setHostHandler(''))
  dispatch(setPartsLoadedHandler(0))
  dispatch(setCurrentStepHandler(0))
  dispatch(setLoadedHandler(false))
  dispatch(removeTaskHandler())
}

function getStepsPath (taskId, part, dispatch, getState) {
  dispatch(setLoadingHandler(true))

  return axios({
    method: 'Get',
    url: '/values/get/' + taskId + '/' + part
  }).then(function (response) {
    var data = getState().dashboard
    var responseData = JSON.parse(response.data)
    if (!_.isNil(responseData)) {
      var paths = _.clone(data.stepsPath)

      for (let path of responseData) {
        paths.push(path)
      }

      dispatch(setStepsPathHandler(paths))

      dispatch(setPartsLoadedHandler(data.partsLoaded + 1))
    }

    data = getState().dashboard
    if (data.partsLoaded === 1) {
      dispatch(setLoadedHandler(true))
    }
    if (data.parts !== data.partsLoaded) {
      setTimeout(function () {
        getStepsPath(data.taskId, data.partsLoaded, dispatch, getState)
      }, 1000)
    } else {
      dispatch(setLoadingHandler(false))
      dispatch(removeTaskHandler())
    }
  }).catch(function (error) {
    if (error) {
      console.log(error)
    }
    dispatch(setLoadingHandler(false))
  })
}

function generateMatrix (x, y, stepsPath, steps, currentStep) {
  var currentStepPath = null
  var stepIndex = 0
  var matrix = new Array(x)
  if (stepsPath !== null && stepsPath.length !== 0) {
    currentStepPath = stepsPath[currentStep]
  }

  for (var i = 0; i < x; i++) {
    matrix[i] = new Array(y)
    for (var j = 0; j < y; j++) {
      if (currentStepPath !== null) {
        matrix[i][j] = currentStepPath[stepIndex]
      } else {
        matrix[i][j] = 0
      }

      stepIndex++
    }
  }

  return matrix
}

function matrixToString (matrix) {
  var result = ''

  for (let row of matrix) {
    for (let col of row) {
      result += col
    }
  }

  return result
}

export const actions = {
  onStepsChangeHandler,
  onPartsChangeHandler,
  onSizeChangeHandler,
  onFieldChangeHandler,
  nextButtonHandler,
  nextButtonMaxHandler,
  prevButtonHandler,
  prevButtonMaxHandler,
  calculateHandler,
  resetButtonHandler
}

// ------------------------------------
// Action Handlers
// ------------------------------------
const ACTION_HANDLERS = {
  [HOST_CHANGE]: (state, action) => Object.assign({}, state, {
    host: action.payload
  }),
  [PARTS_LOADED_CHANGE]: (state, action) => Object.assign({}, state, {
    partsLoaded: action.payload
  }),
  [TASK_ID_CHANGE]: (state, action) => Object.assign({}, state, {
    taskId: action.payload
  }),
  [STEPS_PATH_CHANGE]: (state, action) => Object.assign({}, state, {
    stepsPath: action.payload
  }),
  [CURRENT_STEP_CHANGE]: (state, action) => Object.assign({}, state, {
    currentStep: action.payload
  }),
  [FIELD_CHANGE]: (state, action) => Object.assign({}, state, {
    field: action.payload
  }),
  [SET_LOADING]: (state, action) => Object.assign({}, state, {
    loading: action.payload
  }),
  [SET_LOADED]: (state, action) => Object.assign({}, state, {
    loaded: action.payload
  }),
  [STEPS_CHANGE]: (state, action) => Object.assign({}, state, {
    steps: action.payload
  }),
  [PARTS_CHANGE]: (state, action) => Object.assign({}, state, {
    parts: action.payload
  }),
  [SIZE_CHANGE]: (state, action) => {
    return Object.assign({}, state, {
      field: generateMatrix(action.payload, action.payload, state.stepsPath, state.steps, state.currentStep),
      fieldSize: action.payload
    })
  },
  [FIELD_INPUT_CHANGE]: (state, action) => {
    var field = _.map(state.field, _.clone)
    field[action.x][action.y] = action.payload ? 1 : 0

    return Object.assign({}, state, {
      field: field
    })
  }
}
function getIitialState () {
  const fieldSize = 20

  return {
    currentStep: 0,
    steps: 5,
    parts: 1,
    fieldSize: fieldSize,
    stepsPath: [],
    field: generateMatrix(fieldSize, fieldSize, null, 5, 0),
    loading: false,
    loaded: false,
    partsLoaded: 0,
    taskId: null,
    host: ''
  }
}

const initialState = getIitialState()

export default function dahsboardReducer (state = initialState, action) {
  const handler = ACTION_HANDLERS[action.type]
  return handler ? handler(state, action) : state
}
