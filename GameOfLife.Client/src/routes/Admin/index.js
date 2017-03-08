import {
  injectReducer
} from '../../store/reducers'
import {
  loadHostsOnEnter,
  stopFetchingOnChange
} from '../routeEnter'

export default (store) => ({
  path: '/admin',
  onEnter: loadHostsOnEnter(store),
  onLeave: stopFetchingOnChange(store),
  /*  Async getComponent is only invoked when route matches   */
  getComponent (nextState, cb) {
    /*  Webpack - use 'require.ensure' to create a split point
        and embed an async module loader (jsonp) when bundling   */
    require.ensure([], (require) => {
      /*  Webpack - use require callback to define
          dependencies for bundling   */
      const register = require('./containers/AdminContainer').default
      const reducer = require('./modules/admin').default

      /*  Add the reducer to the store on key 'counter'  */
      injectReducer(store, {
        key: 'admin',
        reducer
      })

      /*  Return getComponent   */
      cb(null, register)

      /* Webpack named bundle   */
    }, 'admin')
  }
})
