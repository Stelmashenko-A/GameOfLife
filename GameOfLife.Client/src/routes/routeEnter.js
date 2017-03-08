import {
  setHostsHandler,
  setFetchingHandler
} from './Admin/modules/admin'
import axios from 'axios'

export const loadHostsOnEnter = (store) => (nextState, replace) => {
  axios({
    method: 'Get',
    url: '/values/hosts'
  }).then(function (response) {
    store.dispatch(setHostsHandler(response.data.Items))
    store.dispatch(setFetchingHandler(true))
  })
}

export const stopFetchingOnChange = (store) => (nextState, replace) => {
  store.dispatch(setFetchingHandler(false))
}
