import axios from 'axios'
import _ from 'lodash'

export const HOSTS_CHANGE = 'HOSTS_CHANGE'
export const SET_FETCHING = 'SET_FETCHING'

// ------------------------------------
// Actions
// ------------------------------------
export const setHostsHandler = (hosts) => {
  return {
    type: HOSTS_CHANGE,
    payload: hosts
  }
}

export const setFetchingHandler = (fetch) => {
  return (dispatch, getState) => {
    dispatch({
      type: SET_FETCHING,
      payload: fetch
    })

    if (fetch) {
      fetchHosts(dispatch, getState)
    }
  }
}

function fetchHosts (dispatch, getState) {
  var state = getState().admin
  if (_.isNil(state) || state.fetch) {
    _.delay(function () {
      state = getState().admin
      if (state.fetch) {
        axios({
          method: 'Get',
          url: '/values/hosts'
        }).then(function (response) {
          dispatch(setHostsHandler(response.data.Items))
          fetchHosts(dispatch, getState)
        })
      }
    }, 2000)
  }
}

export const actions = {
  setHostsHandler
}

// ------------------------------------
// Action Handlers
// ------------------------------------
const ACTION_HANDLERS = {
  [HOSTS_CHANGE]: (state, action) => Object.assign({}, state, {
    hosts: action.payload
  }),
  [SET_FETCHING]: (state, action) => Object.assign({}, state, {
    fetch: action.payload
  })
}

const initialState = {
  hosts: [],
  fetch: false
}

export default function adminReducer (state = initialState, action) {
  const handler = ACTION_HANDLERS[action.type]
  return handler ? handler(state, action) : state
}
