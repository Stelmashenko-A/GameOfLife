import axios from 'axios'
import _ from 'lodash'

// ------------------------------------
// Constants
// ------------------------------------
export const STEPS_CHANGE = 'STEPS_CHANGE',
  PARTS_CHANGE = 'PARTS_CHANGE',
  SIZE_CHANGE = 'WIDTH_CHANGE',
  FIELD_INPUT_CHANGE = 'FIELD_INPUT_CHANGE',
  SET_LOADING = 'SET_LOADING',
  SET_LOADED = 'SET_LOADED',
  FIELD_CHANGE = 'FIELD_CHANGE',
  CURRENT_STEP_CHANGE = 'CURRENT_STEP_CHANGE',
  STEPS_PATH_CHANGE = 'STEPS_PATH_CHANGE',
  TASK_ID_CHANGE = 'TASK_ID_CHANGE',
  PARTS_LOADED_CHANGE = 'PARTS_LOADED_CHANGE';

// ------------------------------------
// Actions
// ------------------------------------
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
  return {
    type: STEPS_CHANGE,
    payload: e.target.value
  }
}

export const onPartsChangeHandler = (e) => {
  return {
    type: PARTS_CHANGE,
    payload: e.target.value
  }
}

export const onSizeChangeHandler = (e) => {
  var payload = e.target.value ? e.target.value : 0;
  return {
    type: SIZE_CHANGE,
    payload: payload
  }
}

export const onFieldChangeHandler = (e) => {
  return {
    type: FIELD_INPUT_CHANGE,
    payload: !((" " + e.target.className + " ").replace(/[\n\t]/g, " ").indexOf("active") > -1),
    x: e.target.getAttribute('x'),
    y: e.target.getAttribute('y')
  }
}

// ------------------------------------
// Functions
// ------------------------------------
export const nextButtonHandler = (e) => {
  e.preventDefault();
  return (dispatch, getState) => {
    var state = getState().dashboard;

    if (state.currentStep == state.steps)
      return;

    dispatch(setCurrentStepHandler(state.currentStep + 1));

    state = getState().dashboard;

    var matrix = generateMatrix(state.fieldSize, state.fieldSize, state.stepsPath, state.steps, state.currentStep);

    dispatch(setFieldHandler(matrix));
  }
};

export const nextButtonMaxHandler = (e) => {
  e.preventDefault();
  return (dispatch, getState) => {
    var state = getState().dashboard;

    if (state.currentStep == state.steps)
      return;

    dispatch(setLoadingHandler(true));

    for (var i = 0; i < state.steps - state.currentStep; i++) {
      setTimeout(() => {
        dispatch(setCurrentStepHandler(state.currentStep + 1));

        state = getState().dashboard;

        var matrix = generateMatrix(state.fieldSize, state.fieldSize, state.stepsPath, state.steps, state.currentStep);

        dispatch(setFieldHandler(matrix));

        if (state.currentStep == state.steps)
          dispatch(setLoadingHandler(false));
      }, 500 * i);
    }
  }
};

export const prevButtonHandler = (e) => {
  e.preventDefault();
  return (dispatch, getState) => {
    var state = getState().dashboard;

    if (state.currentStep === 0)
      return;

    dispatch(setCurrentStepHandler(state.currentStep - 1));

    state = getState().dashboard;
    var matrix = generateMatrix(state.fieldSize, state.fieldSize, state.stepsPath, state.steps, state.currentStep);

    dispatch(setFieldHandler(matrix));
  }

};

export const prevButtonMaxHandler = (e) => {
  e.preventDefault();
  return (dispatch, getState) => {
    var state = getState().dashboard;

    if (state.currentStep == 0)
      return;

    dispatch(setLoadingHandler(true));

    for (var i = 0; i < state.currentStep; i++) {
      setTimeout(() => {
        dispatch(setCurrentStepHandler(state.currentStep - 1));

        state = getState().dashboard;

        var matrix = generateMatrix(state.fieldSize, state.fieldSize, state.stepsPath, state.steps, state.currentStep);

        dispatch(setFieldHandler(matrix));

        if (state.currentStep == 0)
          dispatch(setLoadingHandler(false));

      }, 500 * i);
    }
  }

};
export const calculateHandler = (e) => {
  e.preventDefault();
  return (dispatch, getState) => {
    dispatch(setLoadingHandler(true));
	var data = getState().dashboard;

	var path = _.clone(data.stepsPath);
	path.push(matrixToString(data.field))

	dispatch(setStepsPathHandler(path));

	return axios({
        method: 'Post',
        url: '/values/process',
        data: {
          Id: null,
          Field: matrixToString(data.field),
          Steps: data.steps,
          Parts: data.parts,
        }
      })
      .then(function(response) {
        if (response.data != null) {
          dispatch(setLoadingHandler(false));
          dispatch(setTaskIdHandler(response.data));

          data = getState().dashboard;

          getStepsPath(data.taskId, data.parts - data.partsLoaded - 1, data.partsLoaded, dispatch, getState);
        }

		dispatch(setLoadingHandler(false));
      }).catch(function(error) {
        dispatch(setLoadingHandler(false));
      });
  }
}

function getStepsPath(taskId, part, partsLoaded, dispatch, getState) {
  dispatch(setLoadingHandler(true));

  return axios({
      method: 'Get',
      url: '/values/get/' + taskId + '/' + part
    })
    .then(function(response) {
		var data = getState().dashboard;

		if (response.data != "\"null\"") {
			var preParse = JSON.parse(response.data);
			var result = JSON.parse(preParse);

			dispatch(setLoadingHandler(false));

			var paths = _.clone(data.stepsPath);

			for (let path of result) {
				paths.push(path);
			}

			dispatch(setStepsPathHandler(paths));

			dispatch(setPartsLoadedHandler(partsLoaded + 1));
		}

		data = getState().dashboard;

		if (data.parts != data.partsLoaded) {
		  setTimeout(function() {
			getStepsPath(data.taskId, data.parts - data.partsLoaded, data.partsLoaded, dispatch, getState);
		  }, 1000);
		} else {
		  dispatch(setLoadedHandler(true));
		}
    }).catch(function(error) {
      dispatch(setLoadingHandler(false));
    });
}


function generateMatrix(x, y, stepsPath, steps, currentStep) {
  var currentStepPath = null,
    stepIndex = 0;
  var matrix = new Array(x);
  if (stepsPath !== null && stepsPath.length !== 0)
    currentStepPath = stepsPath[currentStep];

  for (var i = 0; i < x; i++) {
    matrix[i] = new Array(y);
    for (var j = 0; j < y; j++) {
      if (currentStepPath !== null) {
        matrix[i][j] = currentStepPath[stepIndex];

      } else {
        matrix[i][j] = 0;
      }

      stepIndex++;
    }
  }

  return matrix;
}

function matrixToString(matrix) {
  var result = '';

  for (let row of matrix) {
    for (let col of row) {
      result += col;
    }
  }

  return result;
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
  calculateHandler
}

// ------------------------------------
// Action Handlers
// ------------------------------------
const ACTION_HANDLERS = {
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
    });
  },
  [FIELD_INPUT_CHANGE]: (state, action) => {
    var field = _.map(state.field, _.clone);
    field[action.x][action.y] = action.payload ? 1 : 0;

    return Object.assign({}, state, {
      field: field
    });
  }
}

function getIitialState() {
  const fieldSize = 5;

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
  };
}

const initialState = getIitialState();

export default function dahsboardReducer(state = initialState, action) {
  const handler = ACTION_HANDLERS[action.type]
  return handler ? handler(state, action) : state
}
